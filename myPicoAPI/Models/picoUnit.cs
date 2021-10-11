using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace myPicoAPI.Models {
    public class picoUnit {
        
        [Key]
        public int UnitId { get; set; }
        public int ownerId { get; set; }
        public string picoUnitNumber { get; set; }
        public float LowSeasonRent { get; set; }
        public float MidSeasonRent { get; set; }
        public float HighSeasonRent { get; set; }
        public float DiscountPercentage { get; set; }
        public string Iban { get; set; }
        public string BankAddress { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string Swift { get; set; } // RABONLAMFBS 4 letters banc code, 2 letters landcode, 2 getal/letter locatiecode, 3 letters branch
        public string Caretaker { get; set; }
        public string CaretakerMobile { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public picoUnit () {
            Appointments = new Collection<Appointment> ();
        }

    }
}