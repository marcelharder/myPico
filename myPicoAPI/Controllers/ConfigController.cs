using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using myPicoAPI.Dtos;
using myPicoAPI.Helpers;
using myPicoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myPicoAPI.Data;

namespace myPicoAPI.Controllers {
    [Authorize]
   // [ServiceFilter (typeof (LogUserActivity))]

    public class ConfigController : Controller {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        private IGeneralStuff _stuff;
        public ConfigController (IDatingRepository repo, IMapper mapper, IGeneralStuff stuff) {
            _mapper = mapper;
            _repo = repo;
            _stuff = stuff;
        }

        [Route ("api/season/{picoUnit}/{year}/{month}")]
        public async Task<IActionResult> getSeason (int picoUnit, int year, int month) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            var selectedUnit = await _repo.GetPicoUnit (picoUnit);
            if (currentUserId != selectedUnit.ownerId) return Unauthorized ();
            var gen = new General (_repo, _mapper);
            var season = gen.getSeasonForThisMonth (year, month);
            var p = _mapper.Map<SeasonForReturnDTO> (season);
            return Ok (p);
        }

       

        [HttpPost]
        [Route ("api/season/{userId}")]
        public IActionResult postSeason (int userId, [FromBody] SeasonForReturnDTO help) {
            var currentUserId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);
            if (currentUserId != userId) return Unauthorized ();
            var gen = new General (_repo, _mapper);
            gen.saveSeason (help);
            return Ok ();
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("api/currency/{amount}/{d}/{ih}")]
        public async Task<IActionResult> getCurrencyChangeAsync(float amount, string d, string ih){
           var result = await _stuff.convertCurrency(amount, d, ih);
           return Ok(result);
        }

        

        
    }
}