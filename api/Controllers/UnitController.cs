using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using myPicoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using myPicoAPI.Data;

namespace myPicoAPI.Controllers {

    public class UnitController : Controller {
        private readonly IMapper _mapper;
        private readonly IUnit _repo;
        public UnitController (IUnit repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        [Route ("api/unitManaged/{userId}")]
        public async Task<IActionResult> getPicoUnitForThisOwner (int userId) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var picoUnit = await _repo.GetPicoUnitForThisUser (userId);

            return Ok (picoUnit);
        }

        [Route ("api/unitDetails/{unitId}")]
        public async Task<IActionResult> getPicoUnitDetails (int userId, int unitId) {
          //  var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
          //  if (currentUserId != userId) return Unauthorized ();
            var picoUnit = await _repo.GetPicoUnit (unitId);
            return Ok (picoUnit);
        }


        [Route ("api/getUnitID/{picoNumber}")]
        public async Task<IActionResult> getUnitId(string picoNumber){
            var help = await _repo.GetPicoUnitId(picoNumber);
            return Ok(help);
        }
        [Route ("api/getUnitPrice/{picoNumber}/{currency}/{day}/{month}")]
        public async Task<IActionResult> getUnitId(int picoNumber, string currency, int day, int month){
            var help = await _repo.GetPicoUnitPrice(picoNumber,currency,day,month);
            return Ok(help);
        }

        [Route ("api/getUnitName/{picoNumber}")]
        public async Task<IActionResult> getUnitName(int picoNumber){
            var help = await _repo.GetPicoUnitName(picoNumber);
            return Ok(help);
        }

        [HttpGet]
        [Route("api/unitPictures/{id}")]
        public async Task<IActionResult> getUnitPictures(int id){
            
            var result = new List<string>();
            await Task.Run(async ()=>{
               // get the unit naam van de unit no
               var help = await _repo.GetPicoUnitName(id);
               // get the strings from the json file
               result = await _repo.getUnitPictures(help);
            });
            return Ok(result);
        }

        [HttpPost]
        [Route ("api/unitDetails/{userId}")]
        public async Task<IActionResult> savePicoUnitDetails (int userId, [FromBody] picoUnit pic) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var picoUnit = await _repo.GetPicoUnit (pic.UnitId);
            picoUnit.LowSeasonRent = pic.LowSeasonRent;
            picoUnit.MidSeasonRent = pic.MidSeasonRent;
            picoUnit.HighSeasonRent = pic.HighSeasonRent;
            if (await _repo.SaveAll ()) { return Ok (picoUnit); }
            return BadRequest ();
        }



        [Route ("api/appartmentUsers/{appartmentId}/{userId}")]
        public async Task<IActionResult> getAppartmentUsers (int appartmentId, int userId) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var appartmentUsers = new List<User> ();
            appartmentUsers = await _repo.getAppartmentUsers (appartmentId);
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>> (appartmentUsers);
            return Ok (usersToReturn);
        }

    }
}