using System;
using System.Net.Mail;

namespace myPicoAPI.Data {
    public class ClassEMailMailGun : IMail {
        public string sendEmail (EmailFormModel model) {
            var result = "";
            try {
                MailMessage mail = new MailMessage ();
                mail.To.Add (model.To);
                mail.From = new MailAddress ("postmaster@cardiacsoftwaredevelopers.com");
                mail.Subject = model.Subject;
                mail.Body = model.Body;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient ();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential ("postmaster@cardiacsoftwaredevelopers.com", "vereniging");
                client.Host = "smtp.mailgun.org";
                client.Send (mail);
                result = "Succesfully sent the Email.";

            } catch (Exception e) { result = e.Message; }
            return result;
        }
    }
}