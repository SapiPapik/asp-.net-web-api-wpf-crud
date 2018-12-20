using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.BLL.Contracts.Dtos;

namespace ProductCatalog.BLL.Contracts.Contracts {
    public interface IBaseEquipmentService {
        Task<ICollection<EquipmentDto>> GetAllAsync(int limit = 20, int offset = 0);
        Task<EquipmentDto> GetByIdAsync(Guid id);
        Task AddAsync(EquipmentDto equipment);
        Task UpdateAsync(Guid id, EquipmentDto equipment);
        Task RemoveAsync(Guid id);
    }
}
