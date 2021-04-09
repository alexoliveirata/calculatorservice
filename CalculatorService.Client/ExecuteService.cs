using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Client
{
    public static class ExecuteService
    {
        public static async Task<Treturn> Request<T, Treturn>(T model, string operation, string trackingId)
        {
            using (var clientHttp = new HttpClient())
            {
                clientHttp.BaseAddress = new Uri("http://localhost:8081/calculator/");
                clientHttp.DefaultRequestHeaders.Accept.Clear();
                clientHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                clientHttp.DefaultRequestHeaders.Add("X-Evi-Tracking-Id", trackingId);

                string json = JsonConvert.SerializeObject(model, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse = await clientHttp.PostAsync(operation, httpContent);

                string response = await httpResponse.Content.ReadAsStringAsync();

                Treturn jsonResponse = JsonConvert.DeserializeObject<Treturn>(response);

                return jsonResponse;
            }
        }
    }
}
