using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.DAL.Contracts;
using ProductCatalog.DAL.Repositories;

namespace ProductCatalog.DAL {
    public class UnitOfWork : IUnitOfWork, IDisposable {
        private readonly DbContext _context;
        private readonly List<object> _repositories = new List<object>();
        private readonly IBaseCsvRepository _repositoriesCsv;
        private readonly IApiRepository _apiRepository;

        public UnitOfWork(DbContext context, IBaseCsvRepository csvRepository, IApiRepository apiRepository) {
            _context = context;
            _repositoriesCsv = csvRepository;
            _apiRepository = apiRepository;
        }

        public IBaseRepository<T> GetEntityRepository<T>() where T : class {
            var repo = (IBaseRepository<T>)_repositories.SingleOrDefault(r => r is IBaseRepository<T>);
            if (repo == null) {
                _repositories.Add(repo = new EntityRepository<T>(_context));
            }
            return repo;
        }

        public IBaseCsvRepository GetCsvRepository() => _repositoriesCsv;

        public IApiRepository GetApiRepository() => _apiRepository;

        public Task<int> CommitAsync() {
            return _context.SaveChangesAsync();
        }

        public int Commit() {
            return _context.SaveChanges();
        }

        public bool AutoDetectChanges {
            get { return _context.Configuration.AutoDetectChangesEnabled; }
            set { _context.Configuration.AutoDetectChangesEnabled = value; }
        }

        public List<T> ExecuteStoredProcedure<T>(string procedureName) {
            return _context.Database.SqlQuery<T>($"exec {procedureName}").ToList();
        }

        #region IDisposable

        private bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    _context?.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion
    }
}
