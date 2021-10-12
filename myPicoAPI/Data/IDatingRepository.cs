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
        Task<Month_Model> GetMonth(int id);

        Task<dateNumber> GetMonthYear(int m, int y);
        Task<picoUnit> GetPicoUnitForThisUser(int userId);
        Task<dateOccupancy> GetOccupancy(int picoUnit, int id);
        Task<User> GetUser(int id);
        Task<PagedList<Appointment>> getAppointmentsForAdministrator(int picoUnitId, MessageParams messageParams);
        Task<int> GetMonthId(int month, int year);
        Task<Appointment> GetAppointment(int appointmentId);
        Task<bool> IsOwnerOfAnyUnit(int userId);
        Task<int> getUnitIdForThisUser(int userId);
        Task<PagedList<Appointment>> getAppointmentsForUser(int userId, MessageParams messageParams);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Message> GetMessage(int id);
        Task<picoUnit> GetPicoUnit(int picoUnitId);
        Task<int> GetPicoUnitId(string test);
        Task<string> GetPicoUnitName(int test);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
        Task<dateOccupancy> getDateOccupancy(int id);
        Task<List<User>> getAppartmentUsers(int appartmentId);
        Task<int> GetPicoUnitPrice(int picoNumber, string currency, int day, int month);
    }
}