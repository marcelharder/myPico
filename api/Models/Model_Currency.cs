using System;

namespace DatingApp.API.Models
{
    public class Model_Currency
    {
        public int Id { get; set; }

        public DateTime date { get; set; }
        public double USDPHP { get; set; }
        public double EURPHP { get; set; }
        public double JPYPHP { get; set; }
    }
}