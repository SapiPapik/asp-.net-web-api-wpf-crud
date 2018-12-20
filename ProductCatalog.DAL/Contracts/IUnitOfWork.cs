using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Contracts {
    public interface IUnitOfWork {
        IBaseRepository<T> GetEntityRepository<T>() where T : class;
        IBaseCsvRepository GetCsvRepository();
        IApiRepository GetApiRepository();
        Task<int> CommitAsync();
        int Commit();
        bool AutoDetectChanges { get; set; }
        List<T> ExecuteStoredProcedure<T>(string procedureName);
    }
}
