using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using myPicoAPI.Dtos;
using myPicoAPI.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<Model_Occupancy_Date> GetMonthYear(int picoUnitId, int m, int y);
        Task<int> UpdateOccupancy(SeasonForReturnDTO doc);
        Task<int> DeleteOccupancy(int id);
        Task<User> GetUser(int id);
        
        Task<Appointment> GetAppointment(int appointmentId);
        Task<PagedList<Appointment>> getAppointmentsForUser(int userId, MessageParams messageParams);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
        
       
    }
}