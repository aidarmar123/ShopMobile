using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShopMobileWebApp.Models
{
    public partial class Product
    {
        private static string Urlvalute = "https://www.cbr-xml-daily.ru/latest.js";
        private static HttpClient httpClient = new HttpClient();

        public async Task<double> PriceInVolute(string volute)
        {
            if (volute == "RUB")
                return PriceRub;

            var response = await httpClient.GetAsync(Urlvalute);
            var content = await response.Content.ReadAsStringAsync();
            var jsObject = JObject.Parse(content);
            return PriceRub*jsObject["rates"]![volute].Value<double>();
           
        }
        
    }
}
