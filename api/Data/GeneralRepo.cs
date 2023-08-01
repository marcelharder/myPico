using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace myPicoAPI.Data
{
    public class GeneralRepo : IGeneralStuff
    {

      public async Task<help> convertCurrency()
        {
            var h = new help();
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
                h = Newtonsoft.Json.JsonConvert.DeserializeObject<help>(body);
              
            }
         return h;
        }
    }
}



public class help
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
public class quote
{
    public string USDPHP { get; set; }
    public string USDEUR { get; set; }
    public string USDJPY { get; set; }


}


