using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using myPicoAPI.Models;

namespace myPicoAPI.Data {

    public class seedDates {
        private readonly DataContext _repo;
        public seedDates (DataContext repo) {
            _repo = repo;
        }

        public async Task seedAppointments(){
            if(await _repo.Appointments.AnyAsync()) return;
            var appt = new Appointment ();
            var help_appt = Newtonsoft.Json.JsonConvert.DeserializeObject<Appointment[]> (System.IO.File.ReadAllText ("Data/appointmentSeed/app.json"));
            foreach (Appointment app in help_appt) { _repo.Appointments.Add (app); }
            _repo.SaveChanges ();
        }

        public async Task seedUnits(){
            if(await _repo.PicoUnits.AnyAsync()) return;
            var unit = new picoUnit();
            unit.picoUnitNumber = "Myna 610-A";
            _repo.PicoUnits.Add(unit);
            _repo.SaveChanges();
        }
        public async Task SeedDatesAsync () {

            if(await _repo.Months.AnyAsync()) return;
            if(await _repo.DateNumbers.AnyAsync()) return;
            if(await _repo.DateOccupancy.AnyAsync()) return;

           /*  _repo.Months.RemoveRange (_repo.Months);
            //_repo.Appointments.RemoveRange (_repo.Appointments);
            _repo.DateNumbers.RemoveRange (_repo.DateNumbers);
            _repo.DateOccupancy.RemoveRange (_repo.DateOccupancy);
            _repo.SaveChanges (); */
       
            var help = new Model_Year ();
            help = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_Year>
                (System.IO.File.ReadAllText ("Data/dayNumbers/2018/dates-2018.json"));
            var help_occ = new Model_YearOccupancy ();
            help_occ = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_YearOccupancy>
                (System.IO.File.ReadAllText ("Data/occupancy/2018/occupancy-2018.json"));

            updateYear(2018,help,help_occ);

            help = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_Year>
                (System.IO.File.ReadAllText ("Data/dayNumbers/2019/dates-2019.json"));
            help_occ = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_YearOccupancy>
                (System.IO.File.ReadAllText ("Data/occupancy/2019/occupancy-2019.json"));
            
            updateYear(2019,help,help_occ);

            help = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_Year>
                (System.IO.File.ReadAllText ("Data/dayNumbers/2020/dates-2020.json"));
            help_occ = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_YearOccupancy>
                (System.IO.File.ReadAllText ("Data/occupancy/2020/occupancy-2020.json"));

            updateYear(2020,help,help_occ);

            help = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_Year>
                (System.IO.File.ReadAllText ("Data/dayNumbers/2021/dates-2021.json"));
            help_occ = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_YearOccupancy>
                (System.IO.File.ReadAllText ("Data/occupancy/2021/occupancy-2021.json"));
            
            updateYear(2021,help,help_occ);
            

        }

        public void updateYear(int year , Model_Year help, Model_YearOccupancy help_occ){
            updateTheMonths (year, 1, "610-A", 1, help.January, help_occ.January);
            updateTheMonths (year, 2, "610-A", 1, help.February, help_occ.February);
            updateTheMonths (year, 3, "610-A", 1, help.March, help_occ.March);
            updateTheMonths (year, 4, "610-A", 1, help.April, help_occ.April);
            updateTheMonths (year, 5, "610-A", 1, help.May, help_occ.May);
            updateTheMonths (year, 6, "610-A", 1, help.June, help_occ.June);
            updateTheMonths (year, 7, "610-A", 1, help.July, help_occ.July);
            updateTheMonths (year, 8, "610-A", 1, help.August, help_occ.August);
            updateTheMonths (year, 9, "610-A", 1, help.September, help_occ.September);
            updateTheMonths (year, 10, "610-A", 1, help.October, help_occ.October);
            updateTheMonths (year, 11, "610-A", 1, help.November, help_occ.November);
            updateTheMonths (year, 12, "610-A", 1, help.December, help_occ.December);
        }

         public async Task seedUsers()
        {
           if(await _repo.Users.AnyAsync()) return;

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
                _repo.Users.Add(user);
            }
            _repo.SaveChanges();
        }

        private void CreatePasswordhash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            };

        }
        private void updateTheMonths (int year,
            int month, string unit,int userId,
            string[] help, string[] help_occ) {

            var dn = new dateNumber ();dn = AddDateNumber (help, year, month);_repo.DateNumbers.Add (dn);
            
            var occ = new dateOccupancy ();occ = AddDateOccupancy (help_occ, year, month);_repo.DateOccupancy.Add (occ);
         
            _repo.SaveChanges ();
        }

        private dateNumber AddDateNumber (string[] help, int year, int month) {
            var d = new dateNumber ();
            try {
                d.Year = year;
                d.MonthId = month;
                d.day_1 = Convert.ToInt32 (help[0]);
                d.day_2 = Convert.ToInt32 (help[1]);
                d.day_3 = Convert.ToInt32 (help[2]);
                d.day_4 = Convert.ToInt32 (help[3]);
                d.day_5 = Convert.ToInt32 (help[4]);
                d.day_6 = Convert.ToInt32 (help[5]);
                d.day_7 = Convert.ToInt32 (help[6]);
                d.day_8 = Convert.ToInt32 (help[7]);
                d.day_9 = Convert.ToInt32 (help[8]);
                d.day_10 = Convert.ToInt32 (help[9]);
                d.day_11 = Convert.ToInt32 (help[10]);
                d.day_12 = Convert.ToInt32 (help[11]);
                d.day_13 = Convert.ToInt32 (help[12]);
                d.day_14 = Convert.ToInt32 (help[13]);
                d.day_15 = Convert.ToInt32 (help[14]);
                d.day_16 = Convert.ToInt32 (help[15]);
                d.day_17 = Convert.ToInt32 (help[16]);
                d.day_18 = Convert.ToInt32 (help[17]);
                d.day_19 = Convert.ToInt32 (help[18]);
                d.day_20 = Convert.ToInt32 (help[19]);
                d.day_21 = Convert.ToInt32 (help[20]);
                d.day_22 = Convert.ToInt32 (help[21]);
                d.day_23 = Convert.ToInt32 (help[22]);
                d.day_24 = Convert.ToInt32 (help[23]);
                d.day_25 = Convert.ToInt32 (help[24]);
                d.day_26 = Convert.ToInt32 (help[25]);
                d.day_27 = Convert.ToInt32 (help[26]);
                d.day_28 = Convert.ToInt32 (help[27]);
                d.day_29 = Convert.ToInt32 (help[28]);
                d.day_30 = Convert.ToInt32 (help[29]);
                d.day_31 = Convert.ToInt32 (help[30]);
                d.day_32 = Convert.ToInt32 (help[31]);
                d.day_33 = Convert.ToInt32 (help[32]);
                d.day_34 = Convert.ToInt32 (help[33]);
                d.day_35 = Convert.ToInt32 (help[34]);
                d.day_36 = Convert.ToInt32 (help[35]);
                d.day_37 = Convert.ToInt32 (help[36]);
                d.day_38 = Convert.ToInt32 (help[37]);
                d.day_39 = Convert.ToInt32 (help[38]);
                d.day_40 = Convert.ToInt32 (help[39]);
                d.day_41 = Convert.ToInt32 (help[40]);
                d.day_42 = Convert.ToInt32 (help[41]);
            } catch (FormatException e) { Console.WriteLine (e.InnerException); }
            return d;
        }
        private dateOccupancy AddDateOccupancy (string[] help, int year, int month) {
            var d = new dateOccupancy ();
            try {
                d.picoUnit = 1;
                d.Year = year;
                d.MonthId = month;

                d.day_1 = Convert.ToInt32 (help[0]);
                d.day_2 = Convert.ToInt32 (help[1]);
                d.day_3 = Convert.ToInt32 (help[2]);
                d.day_4 = Convert.ToInt32 (help[3]);
                d.day_5 = Convert.ToInt32 (help[4]);
                d.day_6 = Convert.ToInt32 (help[5]);
                d.day_7 = Convert.ToInt32 (help[6]);
                d.day_8 = Convert.ToInt32 (help[7]);
                d.day_9 = Convert.ToInt32 (help[8]);
                d.day_10 = Convert.ToInt32 (help[9]);
                d.day_11 = Convert.ToInt32 (help[10]);
                d.day_12 = Convert.ToInt32 (help[11]);
                d.day_13 = Convert.ToInt32 (help[12]);
                d.day_14 = Convert.ToInt32 (help[13]);
                d.day_15 = Convert.ToInt32 (help[14]);
                d.day_16 = Convert.ToInt32 (help[15]);
                d.day_17 = Convert.ToInt32 (help[16]);
                d.day_18 = Convert.ToInt32 (help[17]);
                d.day_19 = Convert.ToInt32 (help[18]);
                d.day_20 = Convert.ToInt32 (help[19]);
                d.day_21 = Convert.ToInt32 (help[20]);
                d.day_22 = Convert.ToInt32 (help[21]);
                d.day_23 = Convert.ToInt32 (help[22]);
                d.day_24 = Convert.ToInt32 (help[23]);
                d.day_25 = Convert.ToInt32 (help[24]);
                d.day_26 = Convert.ToInt32 (help[25]);
                d.day_27 = Convert.ToInt32 (help[26]);
                d.day_28 = Convert.ToInt32 (help[27]);
                d.day_29 = Convert.ToInt32 (help[28]);
                d.day_30 = Convert.ToInt32 (help[29]);
                d.day_31 = Convert.ToInt32 (help[30]);
                d.day_32 = Convert.ToInt32 (help[31]);
                d.day_33 = Convert.ToInt32 (help[32]);
                d.day_34 = Convert.ToInt32 (help[33]);
                d.day_35 = Convert.ToInt32 (help[34]);
                d.day_36 = Convert.ToInt32 (help[35]);
                d.day_37 = Convert.ToInt32 (help[36]);
                d.day_38 = Convert.ToInt32 (help[37]);
                d.day_39 = Convert.ToInt32 (help[38]);
                d.day_40 = Convert.ToInt32 (help[39]);
                d.day_41 = Convert.ToInt32 (help[40]);
                d.day_42 = Convert.ToInt32 (help[41]);
            } catch (FormatException e) { Console.WriteLine (e.InnerException); }
            return d;
        }

    }

}