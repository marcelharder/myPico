using System;
using myPicoAPI.Models;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public picoUnit PicoUnit { get; set; }
        public int UnitId { get; set; }


    }
}