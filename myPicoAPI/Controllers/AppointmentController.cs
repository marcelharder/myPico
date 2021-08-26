using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using myPicoAPI.Dtos;
using myPicoAPI.Helpers;
using myPicoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace myPicoAPI.Controllers {
    [Authorize]
    [ServiceFilter (typeof (LogUserActivity))]
    //[Route ("api/appts/{userId}/[controller]")]
    public class AppointmentController : Controller {

        private readonly IMapper _mapper;
        private readonly IDatingRepository _repo;
        public AppointmentController (IDatingRepository repo, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet ("{id}", Name = "GetAppointment")]
        [Route ("api/getAppointment/{userId}/{id}")]
        public async Task<IActionResult> GetAppointment (int userId, int id) { // get a specific appointment
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var a = await _repo.GetAppointment (id);
            if (a == null) return NotFound ();
            var b = _mapper.Map<AppointmentForReturnDto> (a);
            return Ok (b);
        }

        [HttpGet]
        [Route ("api/getAppointmentForUser")]
        public async Task<IActionResult> GetAppointmentForUser (MessageParams m) { // get all appointments
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != m.UserId) return Unauthorized ();

            // is this user an owner for a certain Unit
            var pu = 0;
            if (await _repo.IsOwnerOfAnyUnit (m.UserId)) {
                // Yes, he or she is owner of Unit, so get all the appointments for this unit
                pu = await _repo.getUnitIdForThisUser (m.UserId);
               
                var a = await _repo.getAppointmentsForAdministrator (pu, m);
                var b = _mapper.Map<IEnumerable<AppointmentForReturnDto>> (a);
                Response.AddPagination (a.Currentpage,
                    a.PageSize,
                    a.TotalCount,
                    a.TotalPages);
                return Ok (b);
            } else {
                // No, he or she is not, so get the appointments for this user
                var appointmentsFromRepo = await _repo.getAppointmentsForUser (m.UserId, m);
                var appts = _mapper.Map<IEnumerable<AppointmentForReturnDto>> (appointmentsFromRepo);
                Response.AddPagination (appointmentsFromRepo.Currentpage,
                    appointmentsFromRepo.PageSize,
                    appointmentsFromRepo.TotalCount,
                    appointmentsFromRepo.TotalPages);
                return Ok (appts);
            }
        }

        [HttpPost]
        [Route ("api/appt/create/{userId}")]
        public async Task<IActionResult> createAppointment (int userId, [FromBody] AppointmentForCreationDto app) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var cr = new CalculateRent (_repo, _mapper);

            var ap = new Appointment();
            ap = _mapper.Map<Appointment> (app);
            var calcRent = cr.getRentPHP (ap.RequestedDays, ap.StartDate.Year, ap.UnitId);
            ap.Rent = Convert.ToSingle (calcRent);
            ap.Year = ap.StartDate.Year;
            _repo.Add (ap);
            if (await _repo.SaveAll ()) return CreatedAtRoute ("GetAppointment", new{controller = "Appointment" , id = ap.Id}, ap);
            throw new Exception ("Appointment was not saved");
        }

        /* [HttpDelete]
        [Route ("api/appt/delete/{userId}/{id}")]
        public async Task<IActionResult> DeleteAppointment (int id, int userId) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var apptFromRepo = await _repo.GetAppointment (id); 
            
            string[] dna = apptFromRepo.RequestedDays;
            var year = apptFromRepo.Year;
            for (int i = 0; i < dna.Length; i++) {saveOccupancy (Convert.ToInt32 (dna[i]), year, 0);} //saving a zero here, so making the appartment vacant
             
            
            _repo.Delete (apptFromRepo); // remove from database


            if (await _repo.SaveAll ()) return NoContent ();
            throw new Exception ("Appointment was not deleted");
        } */
        //api/appointment/1 PUT
        [HttpPut]
        [Route ("api/appt/update")]
        public async Task<IActionResult> UpdateAppointment ([FromBody] AppointmentForUpdateDto app) {
            if (!ModelState.IsValid) return BadRequest (ModelState);
            // get the current user fron JWT token
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            var appFromRepo = await _repo.GetAppointment (app.Id);
            if (appFromRepo == null) return NotFound ($"Could not find appointment with an Id of {app.Id}");
            if (currentUserId != appFromRepo.userId) return Unauthorized ();
            if (app.comment == null) return BadRequest ("No comments to save");
            appFromRepo.comment = app.comment;
            if (await _repo.SaveAll ()) return NoContent ();
            throw new Exception ($"Updating appointment with an ID of {app.Id} failed on save");

        }

        /* [HttpPost]
        [Route ("api/appt/finalize/{userId}/{id}")]
        public async Task<IActionResult> MarkStatusAsFinal (int userId, int id) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var appt = await _repo.GetAppointment (id);
            appt.Status = 1;
            appt.Paid_InFull = 1;
            await _repo.SaveAll ();
            //and now write the selecteddays to the occupancy table
            //first make array from appt.RequestedDays
            string[] dna = appt.RequestedDays;
            var year = appt.Year;
            for (int i = 0; i < dna.Length; i++) {
                saveOccupancy (Convert.ToInt32 (dna[i]), year, 1);
                await _repo.SaveAll ();
            }
            return NoContent ();
        } */
        private async void saveOccupancy (int id, int type) {
           
            var selectedMonth = await _repo.GetMonth (id); // so this gives the month number of 3 for example
            // get the day from theDate.day, so we can focus on the needed day in the occupancy table
        //    var selectedMonthDateNumber = await _repo.getDateNumber (selectedMonth.UserId);
         //   var selectedMonthOccupancy = await _repo.getDateOccupancy (selectedMonth.UserId);
         //   var loc = findLocation (selectedMonthDateNumber, selectedMonthOccupancy, theDate.Day, type);

        }
        private int findLocation (dateNumber sel, dateOccupancy docc, int day, int type) {
            var help = 0;
            if (sel.day_1 == day) docc.day_1 = type;
            if (sel.day_2 == day) docc.day_2 = type;
            if (sel.day_3 == day) docc.day_3 = type;
            if (sel.day_4 == day) docc.day_4 = type;
            if (sel.day_5 == day) docc.day_5 = type;
            if (sel.day_6 == day) docc.day_6 = type;
            if (sel.day_7 == day) docc.day_7 = type;
            if (sel.day_8 == day) docc.day_8 = type;
            if (sel.day_9 == day) docc.day_9 = type;
            if (sel.day_10 == day) docc.day_10 = type;
            if (sel.day_11 == day) docc.day_11 = type;
            if (sel.day_12 == day) docc.day_12 = type;
            if (sel.day_13 == day) docc.day_13 = type;
            if (sel.day_14 == day) docc.day_14 = type;
            if (sel.day_15 == day) docc.day_15 = type;
            if (sel.day_16 == day) docc.day_16 = type;
            if (sel.day_17 == day) docc.day_17 = type;
            if (sel.day_18 == day) docc.day_18 = type;
            if (sel.day_19 == day) docc.day_19 = type;
            if (sel.day_20 == day) docc.day_20 = type;
            if (sel.day_21 == day) docc.day_21 = type;
            if (sel.day_22 == day) docc.day_22 = type;
            if (sel.day_23 == day) docc.day_23 = type;
            if (sel.day_24 == day) docc.day_24 = type;
            if (sel.day_25 == day) docc.day_25 = type;
            if (sel.day_26 == day) docc.day_26 = type;
            if (sel.day_27 == day) docc.day_27 = type;
            if (sel.day_28 == day) docc.day_28 = type;
            if (sel.day_29 == day) docc.day_29 = type;
            if (sel.day_30 == day) docc.day_30 = type;
            if (sel.day_31 == day) docc.day_31 = type;

            return help;
        }

    }
}