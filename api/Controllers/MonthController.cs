using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using myPicoAPI.Dtos;
using myPicoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace myPicoAPI.Controllers {

    public class MonthController : Controller {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public MonthController (IDatingRepository repo, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
        }

        [Route ("api/dates/{picoUnit}/{month}/{year}")]
        [HttpGet]
        public async Task<IActionResult> GDN (int picoUnit, int month, int year) {
            var helpMonth = await _repo.GetMonthYear (picoUnit,month, year);
            return Ok (helpMonth);
        }

        [Route ("api/occupancy/update")]
        [HttpPut]
        public async Task<IActionResult> UpdateDateOccupancy ([FromBody] SeasonForReturnDTO doc) {
            var updateResult = await _repo.UpdateOccupancy(doc);
            if(updateResult == 0){return BadRequest("error updating occupancy");}
            else {
                return Ok();
            }
         }
        [Route ("api/occupancy/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDateOccupancy (int id) {
            var updateResult = await _repo.DeleteOccupancy(id);
            if(updateResult == 0){return BadRequest("error updating occupancy");}
            else {
                return Ok();
            }
         }

        
       

        
    }
}