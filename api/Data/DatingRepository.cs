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
using CloudinaryDotNet.Actions;
using System.Net;
using Org.BouncyCastle.Asn1.Ntt;
using System.ComponentModel;

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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            return user;
        }
        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(u => u.Id == id);
            return photo;
        }
        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.IsMain);
        }
        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            List<int> userIds = new List<int>();
            var users = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();
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
        /*  public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
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
         } */
        /*   public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
          {
              var messages = await _context.Messages
                  .Include(u => u.Sender).ThenInclude(p => p.Photos)
                  .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                  .Where(m => (m.RecipientId == userId && m.RecipientDeleted == false && m.SenderId == recipientId) ||
                     (m.RecipientId == recipientId && m.SenderId == userId && m.SenderDeleted == false))
                  .ToListAsync();
              return messages;
          } */
        public async Task<Model_Occupancy_Date> GetMonthYear(int picoUnitId, int month, int year)
        {
            var dn = new List<int>();
            var occ = new List<int>();
            int daysInMonth = DateTime.DaysInMonth(year, month);

            await Task.Run(() =>
            {
                var firstDay = new DateTime(year, month, 1);
                var help = firstDay.DayOfWeek;
                switch (firstDay.DayOfWeek)
                {

                    case DayOfWeek.Monday: dn = fillMonth(0, daysInMonth); break;
                    case DayOfWeek.Tuesday: dn = fillMonth(1, daysInMonth); break;
                    case DayOfWeek.Wednesday: dn = fillMonth(2, daysInMonth); break;
                    case DayOfWeek.Thursday: dn = fillMonth(3, daysInMonth);  break;
                    case DayOfWeek.Friday: dn = fillMonth(4, daysInMonth);  break;
                    case DayOfWeek.Saturday: dn = fillMonth(5, daysInMonth); break;
                    case DayOfWeek.Sunday: dn = fillMonth(6, daysInMonth);  break;
                }

               
            });
            SeasonForReturnDTO sr = await GetOccupancy(picoUnitId, month, year);
            occ.Add(sr.day_1);occ.Add(sr.day_2);occ.Add(sr.day_3);occ.Add(sr.day_4);occ.Add(sr.day_5);
            occ.Add(sr.day_6);occ.Add(sr.day_7);occ.Add(sr.day_8);occ.Add(sr.day_9);occ.Add(sr.day_10);
            occ.Add(sr.day_11);occ.Add(sr.day_12);occ.Add(sr.day_13);occ.Add(sr.day_14);occ.Add(sr.day_15);
            occ.Add(sr.day_16);occ.Add(sr.day_17);occ.Add(sr.day_18);occ.Add(sr.day_19);occ.Add(sr.day_20);
            occ.Add(sr.day_21);occ.Add(sr.day_22);occ.Add(sr.day_23);occ.Add(sr.day_24);occ.Add(sr.day_25);
            occ.Add(sr.day_26);occ.Add(sr.day_27);occ.Add(sr.day_28);occ.Add(sr.day_29);occ.Add(sr.day_30);
            occ.Add(sr.day_31);occ.Add(sr.day_32);occ.Add(sr.day_33);occ.Add(sr.day_34);occ.Add(sr.day_35);
            occ.Add(sr.day_36);occ.Add(sr.day_37);occ.Add(sr.day_38);occ.Add(sr.day_39);occ.Add(sr.day_40);
            occ.Add(sr.day_41);occ.Add(sr.day_42);
          
            return new Model_Occupancy_Date(dn,occ);

        }

        private List<int> getTest(SeasonForReturnDTO help)
        {
            throw new NotImplementedException();
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
                var newrecord = new dateOccupancy();
                var strip = new List<int>();
                for (int x = 0; x < 43; x++) { strip.Add(0); }

                int daysInMonth = DateTime.DaysInMonth(year, month);
                await Task.Run(() =>
                {
                    var firstDay = new DateTime(year, month, 1);
                    var help = firstDay.DayOfWeek;
                    switch (firstDay.DayOfWeek)
                    {
                        case DayOfWeek.Monday: strip = fillOccupancyMonth(strip, 0, daysInMonth); break;
                        case DayOfWeek.Tuesday: strip = fillOccupancyMonth(strip, 1, daysInMonth); break;
                        case DayOfWeek.Wednesday: strip = fillOccupancyMonth(strip, 2, daysInMonth); break;
                        case DayOfWeek.Thursday: strip = fillOccupancyMonth(strip, 3, daysInMonth); break;
                        case DayOfWeek.Friday: strip = fillOccupancyMonth(strip, 4, daysInMonth); break;
                        case DayOfWeek.Saturday: strip = fillOccupancyMonth(strip, 5, daysInMonth); break;
                        case DayOfWeek.Sunday: strip = fillOccupancyMonth(strip, 6, daysInMonth); break;
                    }
                });
                newrecord.MonthId = month;
                newrecord.Year = year;
                newrecord.picoUnit = picoUnit;
                newrecord.day_1 = strip[0];newrecord.day_2 = strip[1]; newrecord.day_3 = strip[2];newrecord.day_4 = strip[3];newrecord.day_5 = strip[4];
                newrecord.day_6 = strip[5];newrecord.day_7 = strip[6]; newrecord.day_8 = strip[7];newrecord.day_9 = strip[8]; newrecord.day_10 = strip[9];
                newrecord.day_11 = strip[10]; newrecord.day_12 = strip[11];newrecord.day_33 = strip[12];newrecord.day_14 = strip[13]; newrecord.day_15 = strip[14];
                newrecord.day_16 = strip[15]; newrecord.day_17 = strip[16];newrecord.day_18 = strip[17];newrecord.day_19 = strip[18]; newrecord.day_20 = strip[19];
                newrecord.day_21 = strip[20];newrecord.day_22 = strip[21];newrecord.day_23 = strip[22];newrecord.day_24 = strip[23]; newrecord.day_25 = strip[24];
                newrecord.day_26 = strip[25];newrecord.day_27 = strip[26];newrecord.day_28 = strip[27];newrecord.day_29 = strip[28]; newrecord.day_30 = strip[29];
                newrecord.day_31 = strip[30];newrecord.day_32 = strip[31];newrecord.day_33 = strip[32];newrecord.day_34 = strip[33]; newrecord.day_35 = strip[34];
                newrecord.day_36 = strip[35];newrecord.day_37 = strip[36];newrecord.day_38 = strip[37];newrecord.day_39 = strip[38]; newrecord.day_40 = strip[39];
                newrecord.day_41 = strip[40];newrecord.day_42 = strip[41];

                _context.DateOccupancy.Add(newrecord);
                if (await SaveAll()) { return _mapper.Map<SeasonForReturnDTO>(newrecord); } else return null;
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







        private List<int> fillMonth(int help, int noDays)
        {
            var helpList = new List<int>();
            var offset = 0;
            for (int a = 0; a < 43; a++) { helpList.Add(0); } // set up with all 0
            for (int a = 0; a <= noDays; a++) { helpList[a] = a; } // start at help with writing


            switch (help)
            {
                case 0: offset = 0; break;
                case 1: offset = 1 + noDays; helpList.Insert(0, 0); break;
                case 2: offset = 2 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); break;
                case 3: offset = 3 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); break;
                case 4: offset = 4 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); break;
                case 5: offset = 5 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); helpList.Insert(4, 0); break;
                case 6: offset = 6 + noDays; helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); helpList.Insert(4, 0); helpList.Insert(5, 0); break;
            }

            for (int a = offset; a < 43; a++) { helpList.Add(0); } // remove any data beyound the dates

            return helpList;
        }
        private List<int> fillOccupancyMonth(List<int> helpList, int help, int noDays)
        {
            var offset = 0;

            switch (help)
            {
                case 0: offset = 0; break;
                case 1: offset = 1 + noDays; helpList.Insert(0, 3); break;
                case 2: offset = 2 + noDays; helpList.Insert(0, 3); helpList.Insert(1, 3); break;
                case 3: offset = 3 + noDays; helpList.Insert(0, 3); helpList.Insert(1, 3); helpList.Insert(2, 3); break;
                case 4: offset = 4 + noDays; helpList.Insert(0, 3); helpList.Insert(1, 3); helpList.Insert(2, 3); helpList.Insert(3, 3); break;
                case 5: offset = 5 + noDays; helpList.Insert(0, 3); helpList.Insert(1, 3); helpList.Insert(2, 3); helpList.Insert(3, 3); helpList.Insert(4, 3); break;
                case 6: offset = 6 + noDays; helpList.Insert(0, 3); helpList.Insert(1, 3); helpList.Insert(2, 3); helpList.Insert(3, 3); helpList.Insert(4, 3); helpList.Insert(5, 3); break;
            }

            var startOcc = offset;
            var startTail = startOcc + noDays + 1;
            for (int a = startTail; a < 43; a++) { helpList.Add(3); } // write 3 to the tails

            return helpList;
        }

        public Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            throw new NotImplementedException();
        }
    }

}