using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using myPicoAPI.Models;

namespace myPicoAPI.Data
{
    public interface IUnit
    {
        Task<picoUnit> GetPicoUnit(int picoUnitId);
        Task<int> GetPicoUnitId(string test);
        Task<string> GetPicoUnitName(int test);
        Task<int> GetPicoUnitPrice(int picoNumber, string currency, int day, int month);
        Task<int> getUnitIdForThisUser(int userId);
        Task<picoUnit> GetPicoUnitForThisUser(int userId);
        Task<bool> IsOwnerOfAnyUnit(int userId);
        Task<List<User>> getAppartmentUsers(int appartmentId);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
    }
}