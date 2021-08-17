using System;
using DatingApp.API.Models;

namespace myPicoAPI.Models
{
    public class Appointment
    {
        public int Id {get; set;}
        public int userId {get; set;}
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string comment {get; set;}
        public int Status { get; set; }
        public string RequestedDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfNights { get; set; }
        public float Rent { get; set; }
        public float DownPayment { get; set; }
        public int Paid_InFull { get; set; }
        public int BookingAlertSent {get; set;}
        public int UnitId { get; set; }
        public picoUnit pu {get; set;}
        
     
}
}