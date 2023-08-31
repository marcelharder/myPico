using System.Collections.Generic;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace myPicoAPI.Models
{
    public class Model_Occupancy_Date
    {

        public int monthId;
        public List<int> dates { get; set; }
        public List<int> occupancy { get; set; }

        public Model_Occupancy_Date(int month, List<int> d, List<int> o)
        {
            monthId = month;
            dates = d;
            occupancy = o;
        }

    }
}