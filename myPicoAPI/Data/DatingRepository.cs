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
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        private IConfiguration _config;

        public DatingRepository(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.UserId == id);
            return user;
        }
        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(u => u.Id == id);
            return photo;
        }
        /* 
                public async Task<IEnumerable<User>> GetUsers()
                {
                var users = await _context.Users.Include(p => p.Photos).ToListAsync();
                   return users;
                } */

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            List<int> userIds = new List<int>();
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();
            // filter out the current loggedin user
            users = users.Where(u => u.UserId != userParams.UserId);
            // filter out the same sex
            users = users.Where(u => u.Gender == userParams.Gender);
            // filter the age
            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var min = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var max = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= min && u.DateOfBirth <= max);
            }
            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Message> GetMessage(int id) { return await _context.Messages.FirstOrDefaultAsync(u => u.Id == id); }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false && u.IsRead == false);
                    break;
            }
            messages = messages.OrderByDescending(d => d.MessageSent);
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => (m.RecipientId == userId && m.RecipientDeleted == false && m.SenderId == recipientId) ||
                   (m.RecipientId == recipientId && m.SenderId == userId && m.SenderDeleted == false))
                .ToListAsync();
            return messages;
        }

        public async Task<dateNumber> GetMonth(int id)
        {

            return await _context.DateNumbers
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<dateNumber> GetMonthYear(int month, int year)
        {
            var dn = new dateNumber();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            await Task.Run(() =>
            {
                var firstDay = new DateTime(year, month, 1);
                var help = firstDay.DayOfWeek;
                switch (firstDay.DayOfWeek)
                {
                    case DayOfWeek.Sunday: dn = fillMonth(dn, 0, daysInMonth); break;
                    case DayOfWeek.Monday: dn = fillMonth(dn, 1, daysInMonth); break;
                    case DayOfWeek.Tuesday: dn = fillMonth(dn, 2, daysInMonth); break;
                    case DayOfWeek.Wednesday: dn = fillMonth(dn, 3, daysInMonth); break;
                    case DayOfWeek.Thursday: dn = fillMonth(dn, 4, daysInMonth); break;
                    case DayOfWeek.Friday: dn = fillMonth(dn, 5, daysInMonth); break;
                    case DayOfWeek.Saturday: dn = fillMonth(dn, 6, daysInMonth); break;
                }
            });
            // get first day of the month in year
            dn.Year = 2021;
            dn.MonthId = month;


            return dn;

        }









        public async Task<Appointment> GetAppointment(int appointmentId)
        {
            return await _context.Appointments.FirstOrDefaultAsync(u => u.Id == appointmentId);
        }

        public async Task<dateOccupancy> GetOccupancy(int picoUnit, int id)
        {
            var result = await _context.DateOccupancy
                .Where(j => j.Id == id)
                .Where(j => j.picoUnit == picoUnit).FirstOrDefaultAsync();
            return result;
        }



        public async Task<PagedList<Appointment>> getAppointmentsForUser(int userId, MessageParams messageParams)
        { // gets the appointments for the requested user
            var appts = _context.Appointments.Where(b => b.userId == userId).AsQueryable();
            appts = appts.OrderByDescending(d => d.StartDate);
            return await PagedList<Appointment>.CreateAsync(appts, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<PagedList<Appointment>> getAppointmentsForAdministrator(int picoUnitId, MessageParams messageParams)
        { // this gets the appointments from a particular pico unit
            var appts = _context.Appointments.Where(b => b.UnitId == picoUnitId).AsQueryable();
            appts = appts.OrderByDescending(d => d.StartDate);
            return await PagedList<Appointment>.CreateAsync(appts, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<bool> IsOwnerOfAnyUnit(int userId)
        {
            return await _context.PicoUnits.Where(p => p.ownerId == userId).AnyAsync();
        }

        public async Task<int> getUnitIdForThisUser(int userId)
        {
            var pico = new picoUnit();
            pico = await _context.PicoUnits.Where(p => p.ownerId == userId).FirstOrDefaultAsync();
            return pico.UnitId;
        }
        public async Task<dateOccupancy> getDateOccupancy(int id)
        {
            return await _context.DateOccupancy.Where(m => m.MonthId == id).FirstOrDefaultAsync();
        }

        public async Task<dateNumber> getDateNumber(int id)
        {
            return await _context.DateNumbers.Where(m => m.MonthId == id).FirstOrDefaultAsync();
        }

        public async Task<picoUnit> GetPicoUnit(int picoUnitId)
        {
            var pico = new picoUnit();
            pico = await _context.PicoUnits.Where(p => p.UnitId == picoUnitId).FirstOrDefaultAsync();
            return pico;
        }

        public async Task<picoUnit> GetPicoUnitForThisUser(int userId)
        {
            var pico = new picoUnit();
            pico = await _context.PicoUnits.Where(p => p.ownerId == userId).FirstOrDefaultAsync();
            return pico;
        }

        public async Task<List<User>> getAppartmentUsers(int appartmentId)
        {
            var help = new List<User>();
            var listOfUserIds = new List<int>();
            var appts = _context.Appointments.Where(p => p.UnitId == appartmentId).AsQueryable();
            foreach (Appointment a in appts) { listOfUserIds.Add(a.userId); }
            foreach (int i in listOfUserIds) { help.Add(await GetUser(i)); }
            return help;
        }

        public async Task<int> GetPicoUnitId(string test)
        {
            var u = await _context.PicoUnits.FirstOrDefaultAsync(x => x.picoUnitNumber == test);
            return u.UnitId;
        }

        public async Task<string> GetPicoUnitName(int test)
        {
            var u = await _context.PicoUnits.FirstOrDefaultAsync(x => x.UnitId == test);
            return u.picoUnitNumber;
        }

        public async Task<int> GetMonthId(int month, int year)
        {
            var result = 0;
            if(await _context.Months.AnyAsync()){
            var help = await _context.Months
               .Where(x => x.MonthId == month)
               .Where(x => x.Year == year).FirstOrDefaultAsync();
               if(help != null){result = help.MonthId;}
            }
            return result;
        }

        public async Task<int> GetPicoUnitPrice(int picoNumber, string currency, int day, int month)
        {
            var price = 0.00;
            var php_usd_conversion = _config.GetSection("php_usd_conversion").Value;
            var selectedUnit = await _context.PicoUnits.FirstOrDefaultAsync(a => a.UnitId == picoNumber);
            // find out which season it is
            var season = getSeason(day, month);
            switch (season)
            {
                case 0: price = selectedUnit.LowSeasonRent; break;
                case 1: price = selectedUnit.MidSeasonRent; break;
                case 2: price = selectedUnit.HighSeasonRent; break;
            }
            if (currency == "USD")
            {
                var conv = 0.00;
                try { conv = Convert.ToDouble(php_usd_conversion); }
                catch (Exception e) { Console.Write(e); }

                price = price / conv;
            }



            return (int)Math.Round(price);
        }
        private int getSeason(int d, int m)
        {
            var help = 0;

            if (m == 11) { if (d > 23 && d < 31) { help = 1; } } // Xmas
            if (m == 6) { if (d > 2 && d < 27) { help = 1; } }   // summer holiday
            if (m == 4) { if (d > 2 && d < 27) { help = 2; } }   // test holiday

            return help;
        }

        private dateNumber fillMonth(dateNumber dn, int help, int noDays)
        {
            var helpList = new List<int>();
            for (int a = 0; a < 43; a++) { helpList.Add(0); } // set up with all zero's
            for (int a = 1; a < noDays; a++) { helpList[a] = a; } // start at help with writing

            switch(help){
                case 0: break; // sunday
                case 1: helpList.RemoveAt(42);helpList.Insert(0,0);break; // monday
                case 2: helpList.RemoveAt(42);helpList.RemoveAt(41);helpList.Insert(0,0);helpList.Insert(1,0);break; // tuesday
                case 3: helpList.RemoveAt(42);helpList.RemoveAt(41);helpList.RemoveAt(40);helpList.Insert(0,0);helpList.Insert(1,0);helpList.Insert(2,0);break;
                case 4: helpList.RemoveAt(42);helpList.RemoveAt(41);helpList.RemoveAt(40);helpList.RemoveAt(39);
                        helpList.Insert(0,0);helpList.Insert(1,0);helpList.Insert(2,0);helpList.Insert(3,0);break;
                case 5: helpList.RemoveAt(42);helpList.RemoveAt(41);helpList.RemoveAt(40);helpList.RemoveAt(39);helpList.RemoveAt(38);
                        helpList.Insert(0,0);helpList.Insert(1,0);helpList.Insert(2,0);helpList.Insert(3,0);helpList.Insert(4,0);break;
            }
            
            dn.day_1 = helpList[0];
            dn.day_2 = helpList[1];
            dn.day_3 = helpList[2];
            dn.day_4 = helpList[3];
            dn.day_5 = helpList[4];
            dn.day_6 = helpList[5];
            dn.day_7 = helpList[6];
            dn.day_8 = helpList[7];
            dn.day_9 = helpList[8];
            dn.day_10 = helpList[9];

            dn.day_11 = helpList[10];
            dn.day_12 = helpList[11];
            dn.day_13 = helpList[12];
            dn.day_14 = helpList[13];
            dn.day_15 = helpList[14];
            dn.day_16 = helpList[15];
            dn.day_17 = helpList[16];
            dn.day_18 = helpList[17];
            dn.day_19 = helpList[18];
            dn.day_20 = helpList[19];

            dn.day_21 = helpList[20];
            dn.day_22 = helpList[21];
            dn.day_23 = helpList[22];
            dn.day_24 = helpList[23];
            dn.day_25 = helpList[24];
            dn.day_26 = helpList[25];
            dn.day_27 = helpList[26];
            dn.day_28 = helpList[27];
            dn.day_29 = helpList[28];
            dn.day_30 = helpList[29];

            dn.day_31 = helpList[30];
            dn.day_32 = helpList[31];
            dn.day_33 = helpList[32];
            dn.day_34 = helpList[33];
            dn.day_35 = helpList[34];
            dn.day_36 = helpList[35];
            dn.day_37 = helpList[36];
            dn.day_38 = helpList[37];
            dn.day_39 = helpList[38];
            dn.day_40 = helpList[39];

            dn.day_41 = helpList[40];
            dn.day_42 = helpList[41];


            


            return dn;
        }


    }

}