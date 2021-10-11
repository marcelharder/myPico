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

        public async Task seedUnits(){
            if(await _repo.PicoUnits.AnyAsync()) return;
            var unit = new picoUnit();
            unit.picoUnitNumber = "Myna 610-A";
            _repo.PicoUnits.Add(unit);
            _repo.SaveChanges();
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
        
         
    }

}