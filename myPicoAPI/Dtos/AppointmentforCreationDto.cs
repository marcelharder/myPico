using System;

namespace myPicoAPI.Dtos
{
    public class AppointmentForCreationDto
    {
        public string[] RequestedDays { get; set; }
        public int userId {get; set;}
        public int picoUnitId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Day { get; set; }
        public int DownPayment { get; set; }
        public int Month { get; set; }
        public int NoOfNights { get; set; }
        public int Paid_InFull { get; set; }
        public int PicoUnit { get; set; }
        public float Rent { get; set; }
        public int Status { get; set; }
        public int Year { get; set; }
       

        public AppointmentForCreationDto()
        {
           Paid_InFull = 0; 
           Day = 1;
           DownPayment = 0;
           Month = 0;
           NoOfNights = 1;
           Status = 0;
        }
        
}}