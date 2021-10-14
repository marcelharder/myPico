using System;

namespace myPicoAPI.Dtos {
    public class AppointmentForReturnDto {
        public int Id { get; set; }
        public int userId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int PicoUnit { get; set; }
        public int picoUnitId { get; set; }
        public string picoUnitPhotoUrl { get; set; }
        public string Status { get; set; }
        public string comment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfNights { get; set; }
        public float Rent { get; set; }
        public float RentUSD { get; set; }
        public float DownPayment { get; set; }
        public int Paid_InFull { get; set; }
    }
}