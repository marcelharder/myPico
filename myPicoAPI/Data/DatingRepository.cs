using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using myPicoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data {
    public class DatingRepository : IDatingRepository {
        private readonly DataContext _context;
        public DatingRepository (DataContext context) {
            _context = context;

        }
        public void Add<T> (T entity) where T : class {
            _context.Add (entity);
        }

        public void Delete<T> (T entity) where T : class {
            _context.Remove (entity);
        }

        public async Task<User> GetUser (int id) {
            var user = await _context.Users.Include (p => p.Photos).FirstOrDefaultAsync (u => u.UserId == id);
            return user;
        }
        public async Task<Photo> GetPhoto (int id) {
            var photo = await _context.Photos.FirstOrDefaultAsync (u => u.Id == id);
            return photo;
        }
        /* 
                public async Task<IEnumerable<User>> GetUsers()
                {
                var users = await _context.Users.Include(p => p.Photos).ToListAsync();
                   return users;
                } */

        public async Task<bool> SaveAll () {
            return await _context.SaveChangesAsync () > 0;
        }

        public async Task<Photo> GetMainPhotoForUser (int userId) {
            return await _context.Photos.Where (u => u.UserId == userId).FirstOrDefaultAsync (p => p.IsMain);
        }

        public async Task<PagedList<User>> GetUsers (UserParams userParams) {
            List<int> userIds = new List<int> ();
            var users = _context.Users.Include (p => p.Photos).OrderByDescending (u => u.LastActive).AsQueryable ();
            // filter out the current loggedin user
            users = users.Where (u => u.UserId != userParams.UserId);
            // filter out the same sex
            users = users.Where (u => u.Gender == userParams.Gender);
            // filter the age
            if (userParams.MinAge != 18 || userParams.MaxAge != 99) {
                var min = DateTime.Today.AddYears (-userParams.MaxAge - 1);
                var max = DateTime.Today.AddYears (-userParams.MinAge);

                users = users.Where (u => u.DateOfBirth >= min && u.DateOfBirth <= max);
            }
            if (!string.IsNullOrEmpty (userParams.OrderBy)) {
                switch (userParams.OrderBy) {
                    case "created":
                        users = users.OrderByDescending (u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending (u => u.LastActive);
                        break;
                }
            }
            return await PagedList<User>.CreateAsync (users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Message> GetMessage (int id) { return await _context.Messages.FirstOrDefaultAsync (u => u.Id == id); }

        public async Task<PagedList<Message>> GetMessagesForUser (MessageParams messageParams) {
            var messages = _context.Messages
                .Include (u => u.Sender).ThenInclude (p => p.Photos)
                .Include (u => u.Recipient).ThenInclude (p => p.Photos)
                .AsQueryable ();

            switch (messageParams.MessageContainer) {
                case "Inbox":
                    messages = messages.Where (u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where (u => u.SenderId == messageParams.UserId && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where (u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false && u.IsRead == false);
                    break;
            }
            messages = messages.OrderByDescending (d => d.MessageSent);
            return await PagedList<Message>.CreateAsync (messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread (int userId, int recipientId) {
            var messages = await _context.Messages
                .Include (u => u.Sender).ThenInclude (p => p.Photos)
                .Include (u => u.Recipient).ThenInclude (p => p.Photos)
                .Where (m => (m.RecipientId == userId && m.RecipientDeleted == false && m.SenderId == recipientId) ||
                    (m.RecipientId == recipientId && m.SenderId == userId && m.SenderDeleted == false))
                .ToListAsync ();
            return messages;
        }

        public async Task<dateNumber> GetMonth (int year, int month) {
           
            return await _context.DateNumbers
                .Where (x => x.Year == year)
                .Where (j => j.MonthId == month).FirstOrDefaultAsync ();
        }

        public async Task<Appointment> GetAppointment (int appointmentId) {
            return await _context.Appointments.FirstOrDefaultAsync (u => u.Id == appointmentId);
        }

        public async Task<dateOccupancy> GetMonthPerUnit(int picoUnit, int year, int month)
        {
            var result = await _context.DateOccupancy
                .Where (x => x.Year == year)
                .Where (j => j.MonthId == month)
                .Where (j => j.picoUnit == picoUnit).FirstOrDefaultAsync ();
            return result;
        }

       

        public async Task<PagedList<Appointment>> getAppointmentsForUser (int userId, MessageParams messageParams) { // gets the appointments for the requested user
            var appts = _context.Appointments.Where (b => b.userId == userId).AsQueryable ();
            appts = appts.OrderByDescending (d => d.StartDate);
            return await PagedList<Appointment>.CreateAsync (appts, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<PagedList<Appointment>> getAppointmentsForAdministrator (int picoUnitId, MessageParams messageParams) { // this gets the appointments from a particular pico unit
            var appts = _context.Appointments.Where (b => b.UnitId == picoUnitId).AsQueryable ();
            appts = appts.OrderByDescending (d => d.StartDate);
            return await PagedList<Appointment>.CreateAsync (appts, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<bool> IsOwnerOfAnyUnit (int userId) {
            return await _context.PicoUnits.Where (p => p.ownerId == userId).AnyAsync ();
        }

        public async Task<int> getUnitIdForThisUser (int userId) {
            var pico = new picoUnit ();
            pico = await _context.PicoUnits.Where (p => p.ownerId == userId).FirstOrDefaultAsync ();
            return pico.UnitId;
        }
        public async Task<dateOccupancy> getDateOccupancy (int id) {
            return await _context.DateOccupancy.Where (m => m.MonthId == id).FirstOrDefaultAsync ();
        }

        public async Task<dateNumber> getDateNumber (int id) {
            return await _context.DateNumbers.Where (m => m.MonthId == id).FirstOrDefaultAsync ();
        }

        public async Task<picoUnit> GetPicoUnit (int picoUnitId) {
            var pico = new picoUnit ();
            pico = await _context.PicoUnits.Where (p => p.UnitId == picoUnitId).FirstOrDefaultAsync ();
            return pico;
        }

        public async Task<picoUnit> GetPicoUnitForThisUser (int userId) {
            var pico = new picoUnit ();
            pico = await _context.PicoUnits.Where (p => p.ownerId == userId).FirstOrDefaultAsync ();
            return pico;
        }

        public async Task<List<User>> getAppartmentUsers (int appartmentId) {
            var help = new List<User> ();
            var listOfUserIds = new List<int> ();
            var appts = _context.Appointments.Where (p => p.UnitId == appartmentId).AsQueryable ();
            foreach (Appointment a in appts) { listOfUserIds.Add (a.userId); }
            foreach (int i in listOfUserIds) { help.Add (await GetUser (i)); }
            return help;
        }

        
    }
}