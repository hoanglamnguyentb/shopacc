using Hinet.Service.ApiService.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.ApiService
{
    public class ApiService : IApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> GetPostsAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<string> GetTokenFromAPI(APIDto dto)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
                };
                using (var client = new HttpClient(handler))
                {
                    // Prepare the body content with username and password
                    var content = new StringContent(
                        JsonConvert.SerializeObject(new
                        {
                            username = dto.UserName,
                            password = dto.Password
                        }),
                        Encoding.UTF8,
                        "application/json");

                    var response = await client.PostAsync(dto.LinkGetToken, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        var jsonObject = JObject.Parse(responseContent);

                        string accessToken = jsonObject["accessToken"]?.ToString();

                        return accessToken;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<JObject> GetDataFromAPI(string endPoint, string token, JObject payload)
        {
            //var payload = new JObject
            //{
            //    ["Thang"] = "11",
            //    ["Nam"] = "2023"
            //};
            var demoData = @"[data: {
                'coQuanTiepNhanId': 'H02.29.23',
                'tenCoQuanTiepNhan': 'UBND xã Tân Thanh',
                'capTiepNhan': 6,
                'tenNhomTiepNhan': 'Một cửa điện tử',
                'tenNhomDonVi': 'UBND Phường, xã',
                'maNhomDonVi': '6',
                'thang': '11',
                'tongSoHoSo': 2,
                'daXuLy': 1,
                'daXuLySomHan': 2,
                'daXuLyDungHan': 3,
                'MotPhan': 2,
                'ToanTrinh': 4,
                'soHoSoDangXuLy': 5,
                'soHoSoChuaXuLy': 6,
                'soHoSoTrongHan': 7,
                'soHoSoQuaHan': 2,
                'thoiGianTruyenDuLieu': '10 năm',
                'tongTiepNhanTrucTiep': 8,
                'tongTiepNhanTrucTuyen': 2,
                'tongTiepNhanBuuDien': 9,
                'daXuLyTreHan': 10
            }]";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var jsonPayload = payload.ToString(Newtonsoft.Json.Formatting.None);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(endPoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var jsonObject = JObject.Parse(responseContent);
                        var desiredObject = jsonObject["data"]?.FirstOrDefault(obj => obj["donViId"]?.ToString() == "d3e80b78-c873-4261-89ac-2ef277941be0");
                        if (desiredObject != null)
                        {
                            Console.WriteLine($"Found object: {desiredObject}");
                            return (JObject)desiredObject;
                        }
                        else
                        {
                            Console.WriteLine("Desired object not found in API response.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return JObject.Parse(demoData);
        }
    }
}