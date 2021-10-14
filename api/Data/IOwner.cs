using System.Threading.Tasks;
using DatingApp.API.Helpers;
using myPicoAPI.Models;

namespace myPicoAPI.Data
{
    public interface IOwner
    {
        public Task<PagedList<Appointment>> getAppointmentsForAdministrator(int picoUnitId, MessageParams messageParams);

        public Task<bool> approveAppointment(int id);
    }
}
  