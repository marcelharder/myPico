using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using myPicoAPI.Data;
using myPicoAPI.Models;

namespace DatingApp.API.Data
{
    public class Unit : IUnit
    {
        private readonly DataContext _context;

        private IConfiguration _config;

        private IGeneralStuff _gen;

        public Unit(DataContext context, IConfiguration config, IGeneralStuff gen)
        {
            _context = context;
            _config = config;
            _gen = gen;

        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.UserId == id);
            return user;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<List<User>> getAppartmentUsers(int appartmentId)
        {
            var help = new List<User>();
            var listOfUserIds = new List<int>();
            var appts = _context.Appointments.Where(p => p.UnitId == appartmentId).AsQueryable();
            foreach (Appointment a in appts) { listOfUserIds.Add(a.userId); }
            foreach (int i in listOfUserIds) { help.Add(await GetUser(i)); }
            return help;
        }

        public async Task<picoUnit> GetPicoUnit(int picoUnitId)
        {
            var pico = new picoUnit();
            pico = await _context.PicoUnits.Where(p => p.UnitId == picoUnitId).FirstOrDefaultAsync();
            return pico;
        }

        public async Task<picoUnit> GetPicoUnitForThisUser(int userId)
        {
            var pico = new picoUnit();
            pico = await _context.PicoUnits.Where(p => p.ownerId == userId).FirstOrDefaultAsync();
            return pico;
        }

        public async Task<int> GetPicoUnitId(string test)
        {
            var u = await _context.PicoUnits.FirstOrDefaultAsync(x => x.picoUnitNumber == test);
            return u.UnitId;
        }

        public async Task<string> GetPicoUnitName(int test)
        {
            var u = await _context.PicoUnits.FirstOrDefaultAsync(x => x.UnitId == test);
            return u.picoUnitNumber;
        }

        public async Task<string> GetPicoUnitPrice(int picoNumber, string currency, int day, int month)
        {
            var help = 0.00;
            var php_coeff = 0.00;
            var eur_coeff = 0.00;
            var yen_coeff = 0.00;

            // check to see if the currency's are available for this day
            var current_date = DateTime.Today;
            if (await _context.Currency.AnyAsync(a => a.date == current_date))
            {
                // get the stuff from the database
                var h = await _context.Currency.FirstOrDefaultAsync(a => a.date == current_date);
                php_coeff = h.USDPHP;
                eur_coeff = h.USDEUR;
                yen_coeff = h.USDJPY;
            }
            else
            { // get the stuff from the currency api
                var h = await _gen.convertCurrency();

                php_coeff = Convert.ToDouble(h.quotes.USDPHP);
                eur_coeff = Convert.ToDouble(h.quotes.USDEUR);
                yen_coeff = Convert.ToDouble(h.quotes.USDJPY);
                // and make a new record in the database
                var nr = new Model_Currency();
                nr.date = current_date;
                nr.USDPHP = php_coeff;
                nr.USDEUR = eur_coeff;
                nr.USDJPY = yen_coeff;
                _context.Currency.Add(nr);
                if (await SaveAll()) { }
            }

            var price = 0.00F;

            var selectedUnit = await _context.PicoUnits.FirstOrDefaultAsync(a => a.UnitId == picoNumber);
            // find out which season it is
            var season = getSeason(day, month);
            switch (season)
            {
                case 0: price = selectedUnit.LowSeasonRent; break;
                case 1: price = selectedUnit.MidSeasonRent; break;
                case 2: price = selectedUnit.HighSeasonRent; break;
            }

            if (currency == "PHP")
            {
                help = price;
            }
            else
            {
                if (currency == "USD")
                {
                    help = price / php_coeff;
                }
                else
                {
                    if (currency == "EUR")
                    {
                        var usd = price / php_coeff;
                        help = usd * eur_coeff;
                    }
                    else
                    {
                        if (currency == "YEN")
                        {
                            var usd = price / php_coeff;
                            help = usd * yen_coeff;
                        }
                    }
                }
            }


            return help.ToString("#.##");
        }

        public async Task<int> getUnitIdForThisUser(int userId)
        {
            var pico = new picoUnit();
            pico = await _context.PicoUnits.Where(p => p.ownerId == userId).FirstOrDefaultAsync();
            return pico.UnitId;
        }

        public async Task<bool> IsOwnerOfAnyUnit(int userId)
        {
            return await _context.PicoUnits.Where(p => p.ownerId == userId).AnyAsync();
        }


        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private int getSeason(int d, int m)
        {
            var help = 0;

            if (m == 11) { if (d > 23 && d < 31) { help = 1; } } // Xmas
            if (m == 6) { if (d > 2 && d < 27) { help = 1; } }   // summer holiday
            if (m == 4) { if (d > 2 && d < 27) { help = 2; } }   // test holiday

            return help;
        }

        public async Task<List<string>> getUnitPictures(string unitName)
        {
            var result = new unitPictures();
            var units = new List<unitPictures>();
            var help1 = new List<string>();
            await Task.Run(() =>
                          {
                              var appData = System.IO.File.ReadAllText("Data/unitPictures/pictures.json");
                              var picoUnits = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<unitPictures>>(appData);
                              foreach (var p in picoUnits) { units.Add(p); }
                              foreach (unitPictures p in units)
                              {
                                  if (p.unit == unitName)
                                  {
                                      result = p;
                                      var test = p.pictures;
                                      foreach (pics r in test)
                                      {
                                          help1.Add(r.image);
                                      }
                                  }
                              }
                          });

            return help1;
        }

    }




}
namespace DatingApp.API.Data
{
    public class unitPictures
    {
        public string unit { get; set; }
        public pics[] pictures { get; set; }
    }
}

namespace DatingApp.API.Data
{
    public class pics
    {
        public string image { get; set; }
    }
}