using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Contracts {
    public interface IApiRepository {
        Task<ICollection<T>> GetRequest<T>(string urlRequest, Dictionary<string, object> urlParameters = null) where T : class;
        Task<T> GetRequest<T>(string urlRequest, Guid id, Dictionary<string, object> urlParameters  = null) where T : class;
        Task<bool> PostRequest(string urlRequest, object postObject, Dictionary<string, object> urlParameters = null);
        Task<bool> PutRequest(string urlRequest, Guid id, object putObject, Dictionary<string, object> urlParameters = null);
        Task<bool> DeleteRequest(string urlRequest, Guid id, Dictionary<string, object> urlParameters = null);
    }
}