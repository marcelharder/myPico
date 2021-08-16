using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using myPicoAPI.Data;
using myPicoAPI.Data.email;
using myPicoAPI.Data.sms;
using myPicoAPI.Dtos;
using myPicoAPI.Helpers;
using myPicoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace myPicoAPI.Controllers {
    [Authorize]
    [ServiceFilter (typeof (LogUserActivity))]
    public class CommunicationController : Controller {
        private readonly IMapper _mapper;
        private readonly IDatingRepository _repo;
        public CommunicationController (IDatingRepository repo, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost]
        [Route ("api/appt/sendMail/{userId}/{appointmentId}")]
        public async Task<IActionResult> sendInvoiceByEmail (int userId, int appointmentId) {
            // check the user
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();

            // get the appointment
            var a = await _repo.GetAppointment (appointmentId);
            if (a == null) return NotFound ();

            // get the saillante details from the appointment and put them in the message
            var user = await _repo.GetUser (userId);
            var language = 1;

            IMail repo = new ClassEMailMailGun ();
            var model = new EmailFormModel ();

            model.To = user.Email;
            model.Subject = "Booking Pico de loro - Unit 610-A";

            var ft = new FixedTextModel ();
            ft = ft.GetFixedText (language);
            var saltuation = ft.Line_1 + user.KnownAs + ", ";
            model.Body = saltuation + ft.Line_2 + " " +
                ft.Line_3 +
                ft.Line_4 +
                ft.Line_5 +
                ft.Line_6 +
                ft.Line_7 +
                ft.Line_8 +
                ft.Line_9 +
                ft.Line_10 +
                ft.Line_11;

            var result = repo.sendEmail (model);
            var jsonString = JsonConvert.SerializeObject (result);
            return Ok (jsonString);

        }

        [HttpPost]
        [Route ("api/appt/sendSMS/{userId}/{appointmentId}")]
        public async Task<IActionResult> sendInvoiceBySMS (int userId, int appointmentId) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            // get the appointment
            var a = await _repo.GetAppointment (appointmentId);
            if (a == null) return NotFound ();
            //get the user
            var user = await _repo.GetUser (userId);
            // get the saillante details from the appointment and put them in the SMS
            //assuming that english is the current language, thats why attribute is 1
            //calculate the price of the unit
            var language = 1;
            var model = new SMSFormModel ();
            model.Phone = user.Mobile;
            model.User = "m_harder88";
            model.Password = "AYaaDGUSTPDcaI";
            model.api_id = "3582069";
            var ft = new FixedTextModel ();
            ft = ft.GetFixedText (language);
            var saltuation = ft.Line_1 + " " + user.KnownAs + ",";
            model.Body = saltuation + " " +
                ft.Line_2 + " " + "test" + " " +
                ft.Line_3 + " " + "test-2" + ". " +
                ft.Line_4 +
                ft.Line_5 +
                ft.Line_6 +
                ft.Line_7 +
                ft.Line_8 +
                ft.Line_9 +
                ft.Line_10 +
                ft.Line_11;
            ISMS repo = new ClassSMSClickATell ();
            var result = repo.sendSMS (model);
            var jsonString = JsonConvert.SerializeObject (result);
            return Ok (jsonString);

        }

        [HttpPost]
        [Route ("api/appt/sendBookingSMS/{userId}/{appointmentId}")]
        public async Task<IActionResult> sendBookingNotificationBySMS (int userId, int appointmentId) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            // check if this alert is already sent
            var gen = new General (_repo, _mapper);
                if (await gen.getAlertSent(appointmentId)) { // this makes sure that the booking alert SMS can be sent once
                await gen.setAlertSent(appointmentId);
                // get the appointment
                var a = await _repo.GetAppointment (appointmentId);
                if (a == null) return NotFound ();
                //get the user
                var user = await _repo.GetUser (userId);
                // get the saillante details from the appointment and put them in the SMS
                //assuming that english is the current language, thats why attribute is 1
                var language = 1;
                var model = new SMSFormModel ();
                model.Phone = await gen.getCaretakerMobile (a.picoUnitId); // this will be the number of the caretaker
                model.User = "m_harder88";
                model.Password = "AYaaDGUSTPDcaI";
                model.api_id = "3582069";
                var ft = new FixedTextModel ();
                ft = ft.GetFixedText (language);
                var saltuation = ft.Line_1 + " " + user.KnownAs + ",";
                model.Body = saltuation + ", " + "There is a new booking request from: " + await gen.getUserName (a.userId);
                ISMS repo = new ClassSMSClickATell ();
                var result = repo.sendSMS (model);
                var jsonString = JsonConvert.SerializeObject (result);
                return Ok (jsonString);
            }
            return BadRequest ("Already sent this booking alert");
        }
    }
}