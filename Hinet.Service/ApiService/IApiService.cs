using Hinet.Model.IdentityEntities;
using Hinet.Service.ApiService.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.ApiService
{
    public interface IApiService
    {
        Task<JObject> GetDataFromAPI(string endPoint, string token, JObject payload);

        Task<string> GetPostsAsync(string url);

        Task<string> GetTokenFromAPI(APIDto obj);
    }
}