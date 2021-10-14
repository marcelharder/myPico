using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using myPicoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using myPicoAPI.Dtos;
using AutoMapper;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        private IMapper _mapper;

        private IConfiguration _config;

        public DatingRepository(DataContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _config = config;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
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

                    case DayOfWeek.Monday: dn = fillMonth(dn, 0, daysInMonth); break;
                    case DayOfWeek.Tuesday: dn = fillMonth(dn, 1, daysInMonth); break;
                    case DayOfWeek.Wednesday: dn = fillMonth(dn, 2, daysInMonth); break;
                    case DayOfWeek.Thursday: dn = fillMonth(dn, 3, daysInMonth); break;
                    case DayOfWeek.Friday: dn = fillMonth(dn, 4, daysInMonth); break;
                    case DayOfWeek.Saturday: dn = fillMonth(dn, 5, daysInMonth); break;
                    case DayOfWeek.Sunday: dn = fillMonth(dn, 6, daysInMonth); break;
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
        public async Task<SeasonForReturnDTO> GetOccupancy(int picoUnit, int month, int year)
        {
            // if there is no record then now is the time to add one
            var res = await _context.DateOccupancy
                .Where(j => j.MonthId == month)
                .Where(j => j.Year == year)
                .Where(j => j.picoUnit == picoUnit).AnyAsync();

            if (res)
            { // a record was found
                var result = await _context.DateOccupancy
                .Where(j => j.MonthId == month)
                .Where(j => j.Year == year)
                .Where(j => j.picoUnit == picoUnit).FirstOrDefaultAsync();
                return _mapper.Map<SeasonForReturnDTO>(result);
            }
            else
            {
                // a new dateOccupancy object needs markings for off-agenda days to give visual clue which dates can not be booked
                var nr = new dateOccupancy();
                int daysInMonth = DateTime.DaysInMonth(year, month);
                await Task.Run(() =>
                {
                    var firstDay = new DateTime(year, month, 1);
                    var help = firstDay.DayOfWeek;
                    switch (firstDay.DayOfWeek)
                    {

                        case DayOfWeek.Monday: nr = fillOccupancyMonth(nr, 0, daysInMonth); break;
                        case DayOfWeek.Tuesday: nr = fillOccupancyMonth(nr, 1, daysInMonth); break;
                        case DayOfWeek.Wednesday: nr = fillOccupancyMonth(nr, 2, daysInMonth); break;
                        case DayOfWeek.Thursday: nr = fillOccupancyMonth(nr, 3, daysInMonth); break;
                        case DayOfWeek.Friday: nr = fillOccupancyMonth(nr, 4, daysInMonth); break;
                        case DayOfWeek.Saturday: nr = fillOccupancyMonth(nr, 5, daysInMonth); break;
                        case DayOfWeek.Sunday: nr = fillOccupancyMonth(nr, 6, daysInMonth); break;
                    }
                });
                nr.MonthId = month;
                nr.Year = year;
                nr.picoUnit = picoUnit;
                _context.DateOccupancy.Add(nr);
                if (await SaveAll()) { return _mapper.Map<SeasonForReturnDTO>(nr); } else return null;
            }







        }
        public async Task<PagedList<Appointment>> getAppointmentsForUser(int userId, MessageParams messageParams)
        { // gets the appointments for the requested user
            var appts = _context.Appointments.Where(b => b.userId == userId).AsQueryable();
            appts = appts.OrderByDescending(d => d.StartDate);
            return await PagedList<Appointment>.CreateAsync(appts, messageParams.PageNumber, messageParams.PageSize);
        }
        
        public async Task<int> UpdateOccupancy(SeasonForReturnDTO doc)
        {
            var result = 0;
            var selectedRecord = await _context.DateOccupancy.FirstOrDefaultAsync(s => s.Id == doc.Id);
            selectedRecord = _mapper.Map<dateOccupancy>(doc);
            _context.DateOccupancy.Update(selectedRecord);
            if (await SaveAll()) { result = 1; }
            return result;
        }
        public async Task<int> DeleteOccupancy(int id)
        {
            var result = 0;
            var selectedRecord = await _context.DateOccupancy.FirstOrDefaultAsync(s => s.Id == id);
            _context.DateOccupancy.Remove(selectedRecord);
            if (await SaveAll()) { result = 1; }
            return result;
        }
        
        
        
       
        

        
       
        private dateNumber fillMonth(dateNumber dn, int help, int noDays)
        {
            var helpList = new List<int>();
            var offset = 0;
            for (int a = 0; a < 43; a++) { helpList.Add(0); } // set up with all 0
            for (int a = 0; a <= noDays; a++) { helpList[a] = a; } // start at help with writing


            switch (help)
            {
                case 0: offset = 0; break;
                case 1: offset = 1 + noDays; helpList.Insert(0, 0); break;
                case 2: offset = 2 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); break; // tuesday
                case 3: offset = 3 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); break;
                case 4: offset = 4 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); break;
                case 5: offset = 5 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); helpList.Insert(4, 0); break;
                case 6: offset = 6 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); helpList.Insert(4, 0); helpList.Insert(5, 0); break;
            }

            for (int a = offset; a < 43; a++) { helpList.Add(0); } // remove any data beyound the dates

            dn.day_1 = helpList[1];
            dn.day_2 = helpList[2];
            dn.day_3 = helpList[3];
            dn.day_4 = helpList[4];
            dn.day_5 = helpList[5];
            dn.day_6 = helpList[6];
            dn.day_7 = helpList[7];
            dn.day_8 = helpList[8];
            dn.day_9 = helpList[9];
            dn.day_10 = helpList[10];

            dn.day_11 = helpList[11];
            dn.day_12 = helpList[12];
            dn.day_13 = helpList[13];
            dn.day_14 = helpList[14];
            dn.day_15 = helpList[15];
            dn.day_16 = helpList[16];
            dn.day_17 = helpList[17];
            dn.day_18 = helpList[18];
            dn.day_19 = helpList[19];
            dn.day_20 = helpList[20];

            dn.day_21 = helpList[21];
            dn.day_22 = helpList[22];
            dn.day_23 = helpList[23];
            dn.day_24 = helpList[24];
            dn.day_25 = helpList[25];
            dn.day_26 = helpList[26];
            dn.day_27 = helpList[27];
            dn.day_28 = helpList[28];
            dn.day_29 = helpList[29];
            dn.day_30 = helpList[30];

            dn.day_31 = helpList[31];
            dn.day_32 = helpList[32];
            dn.day_33 = helpList[33];
            dn.day_34 = helpList[34];
            dn.day_35 = helpList[35];
            dn.day_36 = helpList[36];
            dn.day_37 = helpList[37];
            dn.day_38 = helpList[38];
            dn.day_39 = helpList[39];
            dn.day_40 = helpList[40];

            dn.day_41 = helpList[41];
            dn.day_42 = helpList[42];





            return dn;
        }
        private dateOccupancy fillOccupancyMonth(dateOccupancy dn, int help, int noDays)
        {
            var helpList = new List<int>();
            var offset = 0;
          switch (help)
            {
                case 0: offset = 0; break;
                case 1: offset = 1 ; helpList.Insert(0, 3); break;
                case 2: offset = 2 ; helpList.Insert(0, 3); helpList.Insert(1, 3); break; 
                case 3: offset = 3 ; helpList.Insert(0, 3); helpList.Insert(1, 3); helpList.Insert(2, 3); break;
                case 4: offset = 4 ; helpList.Insert(0, 3); helpList.Insert(1, 3); helpList.Insert(2, 3); helpList.Insert(3, 3); break;
                case 5: offset = 5 ; helpList.Insert(0, 3); helpList.Insert(1, 3); helpList.Insert(2, 3); helpList.Insert(3, 3); helpList.Insert(4, 3); break;
                case 6: offset = 6 ; helpList.Insert(0, 3); helpList.Insert(1, 3); helpList.Insert(2, 3); helpList.Insert(3, 3); helpList.Insert(4, 3); helpList.Insert(5, 3); break;
            }

            var startOcc = offset + 1;
            var startTail = startOcc + noDays + 1;

            for (int a = startOcc; a < noDays + startOcc; a++) { helpList.Add(0); } // set up with all 0
 
            while(helpList.Count() < 43){helpList.Add(3);}

           // for (int a = startTail; a < 43 ; a++) { helpList.Add(3); } // write 3 to the tails

            dn.day_1 = helpList[1];
            dn.day_2 = helpList[2];
            dn.day_3 = helpList[3];
            dn.day_4 = helpList[4];
            dn.day_5 = helpList[5];
            dn.day_6 = helpList[6];
            dn.day_7 = helpList[7];
            dn.day_8 = helpList[8];
            dn.day_9 = helpList[9];
            dn.day_10 = helpList[10];

            dn.day_11 = helpList[11];
            dn.day_12 = helpList[12];
            dn.day_13 = helpList[13];
            dn.day_14 = helpList[14];
            dn.day_15 = helpList[15];
            dn.day_16 = helpList[16];
            dn.day_17 = helpList[17];
            dn.day_18 = helpList[18];
            dn.day_19 = helpList[19];
            dn.day_20 = helpList[20];

            dn.day_21 = helpList[21];
            dn.day_22 = helpList[22];
            dn.day_23 = helpList[23];
            dn.day_24 = helpList[24];
            dn.day_25 = helpList[25];
            dn.day_26 = helpList[26];
            dn.day_27 = helpList[27];
            dn.day_28 = helpList[28];
            dn.day_29 = helpList[29];
            dn.day_30 = helpList[30];

            dn.day_31 = helpList[31];
            dn.day_32 = helpList[32];
            dn.day_33 = helpList[33];
            dn.day_34 = helpList[34];
            dn.day_35 = helpList[35];
            dn.day_36 = helpList[36];
            dn.day_37 = helpList[37];
            dn.day_38 = helpList[38];
            dn.day_39 = helpList[39];
            dn.day_40 = helpList[40];

            dn.day_41 = helpList[41];
            dn.day_42 = helpList[42];





            return dn;
        }

    }

}