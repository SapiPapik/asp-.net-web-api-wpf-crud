using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.BLL.Contracts.Contracts;
using ProductCatalog.BLL.Contracts.Dtos;
using ProductCatalog.DAL.Contracts;

namespace ProductCatalog.WPF.BLL.Services.Services {
    public class EquipmentApiService : BaseService, IBaseEquipmentService {
        private const string ApiMethod = "equipments";

        public EquipmentApiService(IUnitOfWork uow) : base(uow) {
        }

        public async Task<ICollection<EquipmentDto>> GetAllAsync(int limit = 20, int offset = 0) {
            var parameters = new Dictionary<string, object> { { "limit", limit }, { "offset", offset } };
            var result = await UnitOfWork.GetApiRepository().GetRequest<EquipmentDto>(ApiMethod, parameters);
            return result;
        }

        public async Task<EquipmentDto> GetByIdAsync(Guid id) {
            var result = await UnitOfWork.GetApiRepository().GetRequest<EquipmentDto>(ApiMethod, id);
            return result;
        }

        public async Task AddAsync(EquipmentDto equipment) {
            await UnitOfWork.GetApiRepository().PostRequest(ApiMethod, equipment);
        }

        public async Task UpdateAsync(Guid id, EquipmentDto equipment) {
            await UnitOfWork.GetApiRepository().PutRequest(ApiMethod, id, equipment);
        }

        public async Task RemoveAsync(Guid id) {
            await UnitOfWork.GetApiRepository().DeleteRequest(ApiMethod, id);
        }
    }
}
