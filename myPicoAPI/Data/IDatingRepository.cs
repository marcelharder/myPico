using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using myPicoAPI.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<Month_Model> GetMonth(int year, int month);
        Task<picoUnit> GetPicoUnitForThisUser(int userId);
        Task<Month_Model> GetMonthPerUnit(string picoUnit, int year, int month);
        Task<User> GetUser(int id);
        Task<PagedList<Appointment>> getAppointmentsForAdministrator(int picoUnitId, MessageParams messageParams);
        Task<Appointment> GetAppointment(int appointmentId);
        Task<bool> IsOwnerOfAnyUnit(int userId);
        Task<int> getUnitIdForThisUser(int userId);
        Task<PagedList<Appointment>> getAppointmentsForUser(int userId, MessageParams messageParams);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Message> GetMessage(int id);
        Task<picoUnit> GetPicoUnit(int picoUnitId);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
        Task<dateOccupancy> getDateOccupancy(int id);
        Task<List<User>> getAppartmentUsers(int appartmentId);
        Task<dateNumber> getDateNumber(int id);


    }
}