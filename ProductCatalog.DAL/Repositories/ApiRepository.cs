using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductCatalog.DAL.Contracts;
using RestSharp;

namespace ProductCatalog.DAL.Repositories {
    public class ApiRepository : IApiRepository {
        private readonly RestClient _client;

        public ApiRepository(RestClient restClient) {
            _client = restClient;
        }

        public async Task<ICollection<T>> GetRequest<T>(string urlRequest, Dictionary<string, object> urlParameters = null) where T : class {
            var response = await PerformRequest(urlRequest, urlParameters: urlParameters);
            return JsonConvert.DeserializeObject<ICollection<T>>(response.Content);
        }

        public async Task<T> GetRequest<T>(string urlRequest, Guid id, Dictionary<string, object> urlParameters = null) where T : class {
            var response = await PerformRequest($"{urlRequest}/{id}", urlParameters: urlParameters);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<bool> PostRequest(string urlRequest, object postObject, Dictionary<string, object> urlParameters = null) {
            var response = await PerformRequest(urlRequest, Method.POST, urlParameters, postObject);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> PutRequest(string urlRequest, Guid id, object putObject, Dictionary<string, object> urlParameters = null) {
            var response = await PerformRequest($"{urlRequest}/{id}", Method.PUT, urlParameters, putObject);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteRequest(string urlRequest, Guid id, Dictionary<string, object> urlParameters = null) {
            var response = await PerformRequest($"{urlRequest}/{id}", Method.DELETE, urlParameters);
            return response.StatusCode == HttpStatusCode.OK;
        }

        private async Task<IRestResponse> PerformRequest(string url, Method method = Method.GET, Dictionary<string, object> urlParameters = null, object obj = null) {
            var request = new RestRequest(url, method);
            if (urlParameters != null) {
                foreach (var urlParameter in urlParameters) {
                    request.AddParameter(urlParameter.Key, urlParameter.Value);
                }
            }

            if (obj != null) {
                request.AddObject(obj);
            }

            return await _client.ExecuteTaskAsync(request);
        }
    }
}
