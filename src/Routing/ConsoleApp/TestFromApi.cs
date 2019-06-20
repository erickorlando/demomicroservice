using Dto.Request;
using Dto.Response;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TestFromApi
    {
        private const string MediaType = "application/json";
        private readonly IOptions<AppSettings> _settings;

        public TestFromApi(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        public async Task Execute()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            };
            httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            httpClient.DefaultRequestHeaders.Add("Authorization_App", _settings.Value.Token);

            var result = await httpClient.PostAsync("Routes/GetAllAsync/v1", new StringContent(
                JsonConvert.SerializeObject(new DtoRouteRequest
                {
                    CountryCode = "pe"
                }, Formatting.Indented), Encoding.UTF8, MediaType));

            var responseContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var dtoResult = JsonConvert.DeserializeObject<JsonResponseGeneric<DtoRoutesResponse>>(responseContent);

                if (dtoResult.Body.Success)
                {
                    foreach (var dtoRoute in dtoResult.Body.Routes)
                    {
                        Console.WriteLine($"{dtoRoute.Code} => {dtoRoute.Name}");
                    }
                }
            }
        }
    }
}