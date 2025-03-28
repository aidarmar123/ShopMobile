using System.Text;
using Newtonsoft.Json;

namespace ShopMobileWebApp.Services
{
    public static class NetManager
    {
        private static string URL = "http://localhost:55747/";
        
        private static HttpClient httpClient = new HttpClient();

        public static async Task<T> Get<T>(string path)
        {
            var response = await httpClient.GetAsync(URL + path);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }

        public static async Task<T> Post<T>(string path, T data)
        {
            var jsData = JsonConvert.SerializeObject(data);
            var response = await httpClient.PostAsync(URL + path, new StringContent(jsData, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<HttpResponseMessage> Put<T>(string path, T data)
        {
            var jsData = JsonConvert.SerializeObject(data);
            var response = await httpClient.PutAsync(URL + path, new StringContent(jsData, Encoding.UTF8, "application/json"));
            return response;
        }

        public static async Task<HttpResponseMessage> Delete(string path)
        {

            var response = await httpClient.DeleteAsync(URL + path);
            return response;
        }

    }
}
