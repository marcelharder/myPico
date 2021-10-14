using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using myPicoAPI.Models;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;

        }

        public async Task<int> seedUnits()
        {
            var help = 0;
            await Task.Run(() =>
            {
                _context.PicoUnits.RemoveRange(_context.PicoUnits);
                _context.SaveChanges();

                var appData = System.IO.File.ReadAllText("Data/picoUnitsSeed/app.json");
                var picoUnits = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<picoUnit>>(appData);
                foreach (var p in picoUnits)
                {
                    _context.PicoUnits.Add(p);
                }
                _context.SaveChanges();

            });
            return help;
        }
        public async Task<int> SeedAppointments()
        {
            var help = 0;
            await Task.Run(() =>
            {
                _context.Appointments.RemoveRange(_context.Appointments);
                _context.SaveChanges();

                var appData = System.IO.File.ReadAllText("Data/appointmentSeed/app.json");
                var appointments = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<Appointment>>(appData);
                foreach (var appoint in appointments)
                {
                    _context.Appointments.Add(appoint);
                }
                _context.SaveChanges();

            });
            return help;
        }



        public async Task<int> SeedUsers()
        {
            var help = 0;
            await Task.Run(() =>
            {
                _context.Users.RemoveRange(_context.Users);
                _context.SaveChanges();

                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<User>>(userData);
                foreach (var user in users)
                {
                    //create the password hash
                    byte[] passwordHash, passwordSalt;
                    this.CreatePasswordhash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    _context.Users.Add(user);
                }
                _context.SaveChanges();
            });
            return help;
        }
        private void CreatePasswordhash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            };

        }




    }
}