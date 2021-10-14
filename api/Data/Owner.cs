using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using myPicoAPI.Data;
using myPicoAPI.Models;

namespace DatingApp.API.Data
{
    public class Owner : IOwner
    {

        private DataContext _context;
        public Owner(DataContext context){
            _context = context;
         }
        
        

        public async Task<PagedList<Appointment>> getAppointmentsForAdministrator(int picoUnitId, MessageParams messageParams)
        {
            // this gets the appointments from a particular pico unit
            var appts = _context.Appointments.Where(b => b.UnitId == picoUnitId).AsQueryable();
            appts = appts.OrderByDescending(d => d.StartDate);
            return await PagedList<Appointment>.CreateAsync(appts, messageParams.PageNumber, messageParams.PageSize);
        }

        public Task<bool> approveAppointment(int id)
        {
            throw new System.NotImplementedException();
        }
    }

}