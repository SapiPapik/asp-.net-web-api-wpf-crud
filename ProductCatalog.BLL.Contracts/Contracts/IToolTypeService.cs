using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.BLL.Contracts.Dtos;

namespace ProductCatalog.BLL.Contracts.Contracts {
    public interface IToolTypeService {
        Task<ICollection<ToolTypeDto>> GetAllAsync(int limit = 20, int offset = 0);
        Task<ToolTypeDto> GetByIdAsync(Guid id);
        Task AddAsync(ToolTypeDto toolType);
        Task UpdateAsync(Guid id, ToolTypeDto toolType);
        Task RemoveAsync(Guid id);
    }
}
