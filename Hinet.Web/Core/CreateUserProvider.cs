using Hinet.Web.Areas.UserArea.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Hinet.Web.Core
{
    public class CreateUserProvider
    {
        public static async Task<long?> createUser(CreateVM model)
        {
            try
            {
                var pathGetInfo = WebConfigurationManager.AppSettings["PathCreateUser"];
                var param = new objParam()
                {
                    LoginName = model.UserName,
                    FullName = model.FullName,
                    Email = model.Email,
                    Address = model.Address,
                    Password = "12345678"
                };
                long? resultData = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(pathGetInfo);

                    var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                    var response = client.PostAsync(pathGetInfo, content).Result;

                    response.EnsureSuccessStatusCode();
                    var resultDa2ta = await response.Content.ReadAsAsync<long>();
                    //return resultDa2ta;
                }

                return resultData;
            }
            catch (Exception)
            {
                return null;
            }
            //return null;
        }

        public class objParam
        {
            public string LoginName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Password { get; set; }
        }

        public class objOut
        {
            public long Data { get; set; }
        }
    }
}