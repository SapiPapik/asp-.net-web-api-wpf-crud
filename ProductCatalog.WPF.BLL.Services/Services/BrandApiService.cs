using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.BLL.Contracts.Contracts;
using ProductCatalog.BLL.Contracts.Dtos;
using ProductCatalog.DAL.Contracts;

namespace ProductCatalog.WPF.BLL.Services.Services {
    public class BrandApiService : BaseService, IBrandService {

        private const string ApiMethod = "brands";

        public BrandApiService(IUnitOfWork uow) : base(uow) {
        }

        public async Task<ICollection<BrandDto>> GetAllAsync(int limit = 20, int offset = 0) {
            var parameters = new Dictionary<string, object> { { "limit", limit }, { "offset", offset } };
            var result = await UnitOfWork.GetApiRepository().GetRequest<BrandDto>(ApiMethod, parameters);
            return result;
        }

        public async Task<BrandDto> GetByIdAsync(Guid id) {
            var result = await UnitOfWork.GetApiRepository().GetRequest<BrandDto>(ApiMethod, id);
            return result;
        }

        public async Task AddAsync(BrandDto brand) {
            await UnitOfWork.GetApiRepository().PostRequest(ApiMethod, brand);
        }

        public async Task UpdateAsync(Guid id, BrandDto brand) {
            await UnitOfWork.GetApiRepository().PutRequest(ApiMethod, id, brand);
        }

        public async Task RemoveAsync(Guid id) {
            await UnitOfWork.GetApiRepository().DeleteRequest(ApiMethod, id);
        }
    }
}
