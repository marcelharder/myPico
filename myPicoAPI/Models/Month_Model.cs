using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace myPicoAPI.Models {
    public class Month_Model {

        [Key]
        public int MonthId { get; set; }
        public string PicoUnit { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public ICollection<dateNumber> DateNumbers { get; set; }
        public ICollection<dateOccupancy> DateOccupancy { get; set; }
        public int UserId { get; set; }

    }
}