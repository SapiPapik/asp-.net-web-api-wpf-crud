using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.BLL.Contracts.Contracts;
using ProductCatalog.BLL.Contracts.Dtos;
using ProductCatalog.DAL.Contracts;

namespace ProductCatalog.WPF.BLL.Services.Services {
    public class ToolTypeApiService : BaseService, IToolTypeService {
        private const string ApiMethod = "tooltype";

        public ToolTypeApiService(IUnitOfWork uow) : base(uow) {
        }

        public async Task<ICollection<ToolTypeDto>> GetAllAsync(int limit = 20, int offset = 0) {
            var parameters = new Dictionary<string, object> { { "limit", limit }, { "offset", offset } };
            var result = await UnitOfWork.GetApiRepository().GetRequest<ToolTypeDto>(ApiMethod, parameters);
            return result;
        }

        public async Task<ToolTypeDto> GetByIdAsync(Guid id) {
            var result = await UnitOfWork.GetApiRepository().GetRequest<ToolTypeDto>(ApiMethod, id);
            return result;
        }

        public async Task AddAsync(ToolTypeDto toolType) {
            await UnitOfWork.GetApiRepository().PostRequest(ApiMethod, toolType);
        }

        public async Task UpdateAsync(Guid id, ToolTypeDto toolType) {
            await UnitOfWork.GetApiRepository().PutRequest(ApiMethod, id, toolType);
        }

        public async Task RemoveAsync(Guid id) {
            await UnitOfWork.GetApiRepository().DeleteRequest(ApiMethod, id);
        }
    }
}
