using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using myPicoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace myPicoAPI.Controllers {

    [ServiceFilter (typeof (LogUserActivity))]
    public class UnitController : Controller {
        private readonly IMapper _mapper;
        private readonly IDatingRepository _repo;
        public UnitController (IDatingRepository repo, IMapper mapper) {
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

        [Route ("api/unitDetails/{userId}/{unitId}")]
        public async Task<IActionResult> getPicoUnitDetails (int userId, int unitId) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var picoUnit = await _repo.GetPicoUnit (unitId);
            return Ok (picoUnit);
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