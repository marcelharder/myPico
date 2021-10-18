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
           
            var baseUrl = "http://api.currencylayer.com/live?access_key=" + "b4affeb80bcb4fe9ea9b09c31d41c30a";

            var client = new HttpClient();
            var request = new HttpRequestMessage
              {
            Method = HttpMethod.Get,
             RequestUri = new Uri(baseUrl + "&currencies=" + "JPY,USD,PHP"  )
            // RequestUri = new Uri(baseUrl + "&from=" + inhandCurrency + "&to=" + desiredCurrency + "&amount=" + amount )
              };
           using (var response = await client.SendAsync(request))
             {
                 response.EnsureSuccessStatusCode();
                 var body = await response.Content.ReadAsStringAsync();

                 var h = Newtonsoft.Json.JsonConvert.DeserializeObject<help>(body);

                 Console.WriteLine(body);
             }

            return "";






            /*   var client = new HttpClient();
              var request = new HttpRequestMessage
              {
                  Method = HttpMethod.Get,
                 RequestUri = new Uri("http://api.exchangeratesapi.io/v1/latest?access_key=a7216b0cb4cbdf9fc95a0e5c4e2f6b92"),
                //   RequestUri = new Uri("http://api.exchangeratesapi.io/v1/latest?access_key=a7216b0cb4cbdf9fc95a0e5c4e2f6b92&base=USD"),
               //   RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?to=USD&from=PHP&q=1000.0"),
                 /*  Headers =
      {
          { "x-rapidapi-host", "currency-exchange.p.rapidapi.com" },
          { "x-rapidapi-key", "undefined" },
      }, */
            /*  };
             using (var response = await client.SendAsync(request))
             {
                 response.EnsureSuccessStatusCode();
                 var body = await response.Content.ReadAsStringAsync();

                 var h = Newtonsoft.Json.JsonConvert.DeserializeObject<help>(body);

                 Console.WriteLine(body);
             }

            return "help"; */
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
       public string USDGBP { get; set; }
       public string USDJPY { get; set; }
       

    }


}