using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.BLL.Contracts.Dtos;

namespace ProductCatalog.BLL.Contracts.Contracts {
    public interface IBrandService {
        Task<ICollection<BrandDto>> GetAllAsync(int limit = 20, int offset = 0);
        Task<BrandDto> GetByIdAsync(Guid id);
        Task AddAsync(BrandDto brand);
        Task UpdateAsync(Guid id, BrandDto brand);
        Task RemoveAsync(Guid id);
    }
}
