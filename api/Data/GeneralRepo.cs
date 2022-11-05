using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using myPicoAPI.Data;

namespace myPicoAPI.Data
{

    public class GeneralRepo : IGeneralStuff
    {
        public async Task<string> convertCurrency(float amount, string desiredCurrency, string inhandCurrency)
        {
            var client = new HttpClient();
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
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
               
                var h = Newtonsoft.Json.JsonConvert.DeserializeObject<help>(body);

                Console.WriteLine(body);
            }

           return "help";
        }
    }

    class help{
        public bool success { get; set; }
        public long Time { get; set; }
        public string base1 { get; set; }
        public DateTime date {get; set;}
        public string[] rates {get; set;}

    }
     class rate{
         public string SAR { get; set; }
         public string PHP { get; set; }

     }


}