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
    public class BrandService : BaseService, IBrandService {

        public BrandService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public async Task<ICollection<BrandDto>> GetAllAsync(int limit = 20, int offset = 0) {
            var brands = await UnitOfWork.GetEntityRepository<Brand>().All()
                .Where(b => !b.IsArchive).OrderBy(b => b.CreatedAt).Skip(offset).Take(limit).ToListAsync();
            return Mapper.Map<ICollection<BrandDto>>(brands);
        }

        public async Task<BrandDto> GetByIdAsync(Guid id) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            var brand = await UnitOfWork.GetEntityRepository<Brand>().FindByIdAsync(id);
            if (brand == null || brand.IsArchive) {
                throw new NotFoundException($"Brand with id = {id} is not found.");
            }

            return Mapper.Map<BrandDto>(brand);
        }

        public async Task AddAsync(BrandDto brand) {
            if (brand == null) {
                throw new ArgumentNullException(nameof(brand));
            }

            if (string.IsNullOrEmpty(brand.Name)) {
                throw new ArgumentException($"Brand field 'Name' can't be empty.");
            }

            if (await UnitOfWork.GetEntityRepository<Brand>().AnyAsync(b => b.Name == brand.Name && !b.IsArchive)) {
                throw new ObjectAlreadyExistException($"Brand with name = {brand.Name} already exist.");
            }

            var entity = Mapper.Map<Brand>(brand);
            UnitOfWork.GetEntityRepository<Brand>().Add(entity);
            await UnitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Guid id, BrandDto brand) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            if (brand == null) {
                throw new ArgumentNullException(nameof(brand));
            }

            var entity = await UnitOfWork.GetEntityRepository<Brand>().FindByIdAsync(id);
            if (entity == null || entity.IsArchive) {
                throw new NotFoundException($"Brand with id = {id} not found.");
            }

            entity.Name = string.IsNullOrEmpty(brand.Name) ? entity.Name : brand.Name;
            entity.Description = string.IsNullOrEmpty(brand.Description) ? entity.Description : brand.Description;
            UnitOfWork.GetEntityRepository<Brand>().Update(entity);
            await UnitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Guid id) {
            if (id == Guid.Empty) {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await UnitOfWork.GetEntityRepository<Brand>().FindByIdAsync(id);
            if (entity == null || entity.IsArchive) {
                throw new NotFoundException($"Brand with id = {id} not found.");
            }

            entity.IsArchive = true;
            UnitOfWork.GetEntityRepository<Brand>().Update(entity);
            await UnitOfWork.CommitAsync();
        }
    }
}
