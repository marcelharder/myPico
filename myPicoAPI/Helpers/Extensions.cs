using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatingApp.API.Helpers {
    public static class Extensions {
        public static void AddApplicationError (this Microsoft.AspNetCore.Http.HttpResponse response, string message) {
            response.Headers.Add ("Application-Error", message);
            response.Headers.Add ("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add ("Access-Control-Allow-Origin", "*");
        }
        public static void AddPagination (this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages) {
            var paginationHeader = new PaginationHeader (currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings ();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver ();

            response.Headers.Add ("Pagination", JsonConvert.SerializeObject (paginationHeader, camelCaseFormatter));
            response.Headers.Add ("Access-Control-Expose-Headers", "Pagination");

        }
        public static int CalculateAge (this DateTime theDateTime) {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears (age) > DateTime.Today) age--;
            return age;
        }

        public static float CalculateUSDFromPHP (this float theCashPHP) {
            var conversion_factor = 0.16f;
            return (theCashPHP * conversion_factor);

        }
        public static int CalculateNumberOfDays (this string[] theStringArray) {
            return theStringArray.Count () - 1;
        }

        public static string GetDaysOfTheYear (this string[] theStringArray) {
            var help = theStringArray; // ziet er zo uit ""
            var s = getDOTY (help[0].ToString ());
            for (int i = 1; i < help.Length; i++) {
                s = s + ',' + getDOTY (help[i].ToString ());
            }
            return s;
        }
        public static string getSeasonDescription (this int theTest) {
            var help = "";
            switch (theTest) {
                case 6:
                    help = "low";
                    break;
                case 7:
                    help = "mid";
                    break;
                case 8:
                    help = "high";
                    break;
            }

            return help;
        }

        public static int postSeasonDescription (this string theTest) {
            var help = 0;
            switch (theTest) {
                case "low":
                    help = 6;
                    break;
                case "mid":
                    help = 7;
                    break;
                case "high":
                    help = 8;
                    break;
            }

            return help;
        }

        private static string getDOTY (string test) {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var help = test;
            if (help.Length < 10) { // now there are single digits, which gives a problem with the parse.exact
                help = checkForSingleValue (test);
                var d = DateTime.ParseExact (help, "MM/dd/yyyy", null);
                return d.DayOfYear.ToString ();
            } else {
                var d = DateTime.ParseExact (help, "MM/dd/yyyy", null);
                return d.DayOfYear.ToString ();
            }
        }

        public static string ChangeStatus (this int test) {
            var help = "";
            if (test == 0) { help = "Available"; } else {
                if (test == 1) { help = "Requested"; } else {
                    if (test == 2) { help = "Occupied"; }
                }
            }
            return help;
        }

        private static string checkForSingleValue (string test) {
            // break the string in pieces, to look for single digits
            var brokentest = test.Split ("/");
            var month = brokentest[0];
            var day = brokentest[1];
            var year = brokentest[2];

            if (month.Length == 1) { month = "0" + month; }
            if (day.Length == 1) { day = "0" + day; }

            var help = month + '/' + day + '/' + year;

            return help;
        }

    }
}