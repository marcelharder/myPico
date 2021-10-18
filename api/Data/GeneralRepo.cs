using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using myPicoAPI.Data;
using Newtonsoft.Json;

namespace myPicoAPI.Data
{

    public class GeneralRepo : IGeneralStuff
    {
        public async Task<string> convertCurrency(float amount, string desiredCurrency, string inhandCurrency)
        {
            var result = 0.00;
            var baseUrl = "http://api.currencylayer.com/live?access_key=" + "b4affeb80bcb4fe9ea9b09c31d41c30a";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(baseUrl + "&currencies=" + "JPY,USD,EUR,PHP")
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var h = Newtonsoft.Json.JsonConvert.DeserializeObject<help>(body);

                var php_coeff = Convert.ToDouble(h.quotes.USDPHP);
                var eur_coeff = Convert.ToDouble(h.quotes.USDEUR);
                var yen_coeff = Convert.ToDouble(h.quotes.USDJPY);
             

                if (desiredCurrency == "USD") { result = amount / php_coeff; }
                else
                {
                    if (desiredCurrency == "EUR") { result = amount / eur_coeff; }
                    else
                    {
                        if (desiredCurrency == "YEN") { result = amount / yen_coeff; }
                    }
                }
            }
         return result.ToString();
        }
    }
}



class help
{
    [JsonProperty("success")]
    public bool success { get; set; }
    [JsonProperty("timestamp")]
    public long timestamp { get; set; }
    [JsonProperty("source")]
    public string source { get; set; }
    [JsonProperty("quotes")]
    public quote quotes { get; set; }

}
class quote
{
    public string USDPHP { get; set; }
    public string USDEUR { get; set; }
    public string USDJPY { get; set; }


}


