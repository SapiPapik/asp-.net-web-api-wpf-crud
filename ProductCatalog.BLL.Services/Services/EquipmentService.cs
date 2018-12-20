using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductCatalog.BLL.Contracts.Contracts;
using ProductCatalog.BLL.Contracts.Dtos;
using ProductCatalog.BLL.Services.Exceptions;
using ProductCatalog.BLL.Services.Services.Base;
using ProductCatalog.Data.Entities;
using ProductCatalog.DAL.Contracts;

namespace ProductCatalog.BLL.Services.Services {
    public class EquipmentService : BaseService, IEquipmentService {
        public EquipmentService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public async Task<ICollection<EquipmentDto>> GetAllAsync(int limit = 20, int offset = 0) {
            var equipments = (await UnitOfWork.GetCsvRepository().AllAsync())
                .Where(e => !e.IsArchive).OrderBy(e => e.Id).Skip(offset).Take(limit);
            var result = Mapper.Map<ICollection<EquipmentDto>>(equipments);
            foreach (var equipment in result) {
                equipment.Brand = Mapper.Map<BrandDto>(await UnitOfWork.GetEntityRepository<Brand>().FindByIdAsync(equipment.BrandId));
                equipment.ToolType = Mapper.Map<ToolTypeDto>(await UnitOfWork.GetEntityRepository<ToolType>().FindByIdAsync(equipment.ToolTypeId));
            }

            return result;
        }

        public async Task<EquipmentDto> GetByIdAsync(Guid id) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            var equipment = await UnitOfWork.GetCsvRepository().FindByIdAsync(id);
            if (equipment == null || equipment.IsArchive) {
                throw new NotFoundException($"Equipment with id = {id} not found.");
            }

            var result = Mapper.Map<EquipmentDto>(equipment);
            result.Brand = Mapper.Map<BrandDto>(await UnitOfWork.GetEntityRepository<Brand>().FindByIdAsync(equipment.BrandId));
            result.ToolType = Mapper.Map<ToolTypeDto>(await UnitOfWork.GetEntityRepository<ToolType>().FindByIdAsync(equipment.ToolTypeId));
            return result;
        }

        public async Task AddAsync(EquipmentDto equipment) {
            if (equipment == null) {
                throw new ArgumentNullException(nameof(equipment));
            }

            if (equipment.BrandId == Guid.Empty) {
                throw new ArgumentNullException(nameof(equipment.BrandId));
            }

            if (!await UnitOfWork.GetEntityRepository<Brand>().AnyAsync(b => b.Id == equipment.BrandId)) {
                throw new NotFoundException($"Brand with id = {equipment.BrandId} not found.");
            }

            if (equipment.ToolTypeId == Guid.Empty) {
                throw new ArgumentNullException(nameof(equipment.ToolTypeId));
            }

            if (!await UnitOfWork.GetEntityRepository<ToolType>().AnyAsync(b => b.Id == equipment.ToolTypeId)) {
                throw new NotFoundException($"Brand with id = {equipment.ToolTypeId} not found.");
            }

            var entity = Mapper.Map<Equipment>(equipment);
            await UnitOfWork.GetCsvRepository().AddAsync(entity);
        }

        public async Task UpdateAsync(Guid id, EquipmentDto equipment) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            if (equipment == null) {
                throw new ArgumentNullException(nameof(equipment));
            }

            var entity = await UnitOfWork.GetCsvRepository().FindByIdAsync(id);
            if (entity == null || entity.IsArchive) {
                throw new NotFoundException($"Equipment with id = {id} not found.");
            }

            entity.Specifications = string.IsNullOrEmpty(equipment.Specifications) ? entity.Specifications : equipment.Specifications;
            entity.Price = equipment.Price <= 0 ? entity.Price : equipment.Price;
            entity.BrandId = equipment.BrandId == Guid.Empty ? entity.BrandId : equipment.BrandId;
            entity.ToolTypeId = equipment.ToolTypeId == Guid.Empty ? entity.ToolTypeId : equipment.ToolTypeId;

            await UnitOfWork.GetCsvRepository().UpdateAsync(entity);
        }

        public async Task RemoveAsync(Guid id) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await UnitOfWork.GetCsvRepository().FindByIdAsync(id);
            if (entity == null || entity.IsArchive) {
                throw new NotFoundException($"Equipment with id = {id} not found.");
            }

            entity.IsArchive = true;
            await UnitOfWork.GetCsvRepository().UpdateAsync(entity);
        }

        public async Task ImportFromFile(string filePath) {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("File path can not be null.");
            var entities = await UnitOfWork.GetCsvRepository().AllAsync(filePath);
            foreach (var entity in entities) {
                if (await UnitOfWork.GetEntityRepository<Brand>().AnyAsync(b => b.Id == entity.BrandId && !b.IsArchive) &&
                    await UnitOfWork.GetEntityRepository<ToolType>().AnyAsync(t => t.Id == entity.ToolTypeId && !t.IsArchive)) {

                    if (entity.Id != Guid.Empty) {
                        await UnitOfWork.GetCsvRepository().UpdateAsync(entity);
                    } else {
                        await UnitOfWork.GetCsvRepository().AddAsync(entity);
                    }
                } else {
                    throw new ArgumentException($"Row with BrandId: {entity.BrandId} and ToolTypeId: {entity.ToolTypeId} can not be insert.");
                }
            }
        }
    }
}
