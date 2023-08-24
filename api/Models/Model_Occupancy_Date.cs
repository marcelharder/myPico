using System.Collections.Generic;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace myPicoAPI.Models
{ public class Model_Occupancy_Date {
    public List<int> Dates { get; set; }
    public List<int> Occupancy { get; set; }

    public Model_Occupancy_Date (List<int> d, List<int> o) {
            Dates = d;
            Occupancy = o;
        }
    
}}