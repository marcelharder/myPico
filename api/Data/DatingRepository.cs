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
using Microsoft.EntityFrameworkCore.Query;

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
            var helpList = new List<int>();
            for (int a = 0; a < 43; a++) { helpList.Add(0); } // set up with all 0
            for (int a = 0; a < 43; a++) { occ.Add(0); } // set up with all 0
            for (int a = 0; a < daysInMonth; a++) { helpList[a] = a+1; } // start at zero with writng the day number

            await Task.Run(() =>
            {
                var firstDay = new DateTime(year, month, 1);
                var help = firstDay.DayOfWeek;
                switch (firstDay.DayOfWeek)
                {
                    // depending on the firstday of the week the number of leading empty spaces is different
                    case DayOfWeek.Monday: dn = fillMonth(helpList,0); break;
                    case DayOfWeek.Tuesday: dn = fillMonth(helpList,1); break;
                    case DayOfWeek.Wednesday: dn = fillMonth(helpList,2); break;
                    case DayOfWeek.Thursday: dn = fillMonth(helpList,3);  break;
                    case DayOfWeek.Friday: dn = fillMonth(helpList,4);  break;
                    case DayOfWeek.Saturday: dn = fillMonth(helpList,5); break;
                    case DayOfWeek.Sunday: dn = fillMonth(helpList,6);  break;
                }
            });

            for(int x = 0; x < 43; x++){if(dn[x]==0){occ[x]= 3;}}

          
          
            return new Model_Occupancy_Date(month,dn,occ);

        }

        private List<int> getTest(SeasonForReturnDTO help)
        {
            throw new NotImplementedException();
        }


        public async Task<Appointment> GetAppointment(int appointmentId)
        {
            return await _context.Appointments.FirstOrDefaultAsync(u => u.Id == appointmentId);
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
        private List<int> fillMonth(List<int> helpList, int help)
        {
            var returnList = new List<int>();
            switch (help)
            {
                case 0: returnList = helpList; break;
                case 1: helpList.Insert(0, 0); returnList = helpList.SkipLast(1).ToList();break;
                case 2: helpList.Insert(0, 0); helpList.Insert(1, 0); returnList = helpList.SkipLast(2).ToList();break;
                case 3: helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); returnList = helpList.SkipLast(3).ToList();break;
                case 4: helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); returnList = helpList.SkipLast(4).ToList();break;
                case 5: helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); helpList.Insert(4, 0); returnList = helpList.SkipLast(5).ToList();break;
                case 6: helpList.Insert(0, 0); helpList.Insert(1, 0); helpList.Insert(2, 0); helpList.Insert(3, 0); helpList.Insert(4, 0); helpList.Insert(5, 0); returnList = helpList.SkipLast(6).ToList();break;
            }
             return returnList;
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