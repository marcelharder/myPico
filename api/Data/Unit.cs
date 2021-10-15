using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        private const string V = @"Data/unitPictures/pictures.xml";

        public Unit(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

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

        public async Task<int> GetPicoUnitPrice(int picoNumber, string currency, int day, int month)
        {
            var price = 0.00;
            var php_usd_conversion = _config.GetSection("php_usd_conversion").Value;
            var php_eur_conversion = _config.GetSection("php_eur_conversion").Value;
            var php_yen_conversion = _config.GetSection("php_yen_conversion").Value;
            var selectedUnit = await _context.PicoUnits.FirstOrDefaultAsync(a => a.UnitId == picoNumber);
            // find out which season it is
            var season = getSeason(day, month);
            switch (season)
            {
                case 0: price = selectedUnit.LowSeasonRent; break;
                case 1: price = selectedUnit.MidSeasonRent; break;
                case 2: price = selectedUnit.HighSeasonRent; break;
            }
            if (currency == "USD")
            {
                var conv = 0.00; try { conv = Convert.ToDouble(php_usd_conversion); } catch (Exception e) { Console.Write(e); }
                price = price / conv;
            }
            if (currency == "EURO")
            {
                var conv = 0.00;
                try { conv = Convert.ToDouble(php_eur_conversion); } catch (Exception e) { Console.Write(e); }
                price = price / conv;
            }
            if (currency == "YEN")
            {
                var conv = 0.00;
                try { conv = Convert.ToDouble(php_yen_conversion); } catch (Exception e) { Console.Write(e); }
                price = price / conv;
            }



            return (int)Math.Round(price);
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
            var result = new List<string>();
            await Task.Run(() =>
            {
                // get the picture url's from the different units
                XDocument xdoc = XDocument.Load(V);
                IEnumerable<XElement> el = from t in xdoc.Descendants("unit").Elements("name")
                                           where (string)t.Attribute("id") == unitName
                                           select t;

                foreach (XElement j in el)
                {
                    IEnumerable<XElement> h = from s in el.Descendants ("image") select s;
                    foreach(XElement r in h) {
                        result.Add(r.Element("image").Value);
                    }
           
                };
            });

            return result;



        }
    }
}