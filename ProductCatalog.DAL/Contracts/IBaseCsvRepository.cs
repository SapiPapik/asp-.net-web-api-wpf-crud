using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Data.Entities;

namespace ProductCatalog.DAL.Contracts {
    public interface IBaseCsvRepository {
        Task<ICollection<Equipment>> AllAsync(string filePath = "");
        Task<Equipment> FindByIdAsync(Guid id, string filePath = "");
        Task<bool> AnyAsync(Func<Equipment, bool> predicates, string filePath = "");
        Task AddAsync(Equipment equipment, string filePath = "");
        Task UpdateAsync(Equipment equipment, string filePath = "");
        Task RemoveAsync(Equipment equipment, string filePath = "");
    }
}