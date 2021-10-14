using System.ComponentModel.DataAnnotations;

namespace myPicoAPI.Data {
    public class EmailFormModel {
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}