using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class ToolTypeService : BaseService, IToolTypeService {
        public ToolTypeService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public async Task<ICollection<ToolTypeDto>> GetAllAsync(int limit = 20, int offset = 0) {
            var brands = await UnitOfWork.GetEntityRepository<ToolType>().All()
                .Where(b => !b.IsArchive).OrderBy(b => b.CreatedAt).Skip(offset).Take(limit).ToListAsync();
            return Mapper.Map<ICollection<ToolTypeDto>>(brands);
        }

        public async Task<ToolTypeDto> GetByIdAsync(Guid id) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            var toolType = await UnitOfWork.GetEntityRepository<ToolType>().FindByIdAsync(id);
            if (toolType == null || toolType.IsArchive) {
                throw new NotFoundException($"ToolType with id = {id} not found.");
            }

            return Mapper.Map<ToolTypeDto>(toolType);
        }

        public async Task AddAsync(ToolTypeDto toolType) {
            if (toolType == null) {
                throw new ArgumentNullException(nameof(toolType));
            }

            if (await UnitOfWork.GetEntityRepository<ToolType>().AnyAsync(tt => tt.Name == toolType.Name)) {
                throw new ObjectAlreadyExistException($"ToolType field Name = {toolType.Name} already exist.");
            }

            var entity = Mapper.Map<ToolType>(toolType);
            UnitOfWork.GetEntityRepository<ToolType>().Add(entity);
            await UnitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Guid id, ToolTypeDto toolType) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            if (toolType == null) {
                throw new ArgumentNullException(nameof(toolType));
            }

            var entity = await UnitOfWork.GetEntityRepository<ToolType>().FindByIdAsync(id);
            if (entity == null || entity.IsArchive) {
                throw new NotFoundException($"ToolType with id = {id} not found.");
            }

            entity.Name = string.IsNullOrEmpty(toolType.Name) ? entity.Name : toolType.Name;
            entity.DescriptionApplicationOptions = string.IsNullOrEmpty(toolType.DescriptionApplicationOptions) ?
                entity.DescriptionApplicationOptions :
                toolType.DescriptionApplicationOptions;

            UnitOfWork.GetEntityRepository<ToolType>().Update(entity);
            await UnitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Guid id) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await UnitOfWork.GetEntityRepository<ToolType>().FindByIdAsync(id);
            if (entity == null || entity.IsArchive) {
                throw new NotFoundException($"ToolType with id = {id} not found.");
            }

            entity.IsArchive = true;
            UnitOfWork.GetEntityRepository<ToolType>().Update(entity);
            await UnitOfWork.CommitAsync();
        }
    }
}
