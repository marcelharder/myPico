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

        [Route ("api/getMonthId/{month}/{year}")]
        [HttpGet]
        public async Task<IActionResult> GetDateNumbers (int month, int year) {
            var helpMonth = await _repo.GetMonthId (month, year);
            return Ok (helpMonth);
        }

        [Route ("api/dates/{month}/{year}")]
        [HttpGet]
        public async Task<IActionResult> GDN (int month, int year) {
            var helpMonth = await _repo.GetMonthYear (month, year);
            return Ok (helpMonth);
        }

        [Route ("api/occupancy/{picoUnit}/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDateOccupancy (int picoUnit, int id) {
            var helpMonth = await _repo.GetOccupancy (picoUnit, id);
            return Ok (helpMonth);
        }

        private async void saveOccupancy (int id) {
            var selectedMonth = await _repo.GetMonth (id); // so this gives the month number of 3 for example
            // get the day from theDate.day, so we can focus on the needed day in the occupancy table
         //   var selectedMonthDateNumber = await _repo.getDateNumber (selectedMonth.UserId);
          //  var selectedMonthOccupancy = await _repo.getDateOccupancy (selectedMonth.UserId);
          //  var loc = findLocation (selectedMonthDateNumber, selectedMonthOccupancy, theDate.Day, type);

        }
        private int findLocation (dateNumber sel, dateOccupancy docc, int day, int type) {
            var help = 0;
            if (sel.day_1 == day) docc.day_1 = type;
            if (sel.day_2 == day) docc.day_2 = type;
            if (sel.day_3 == day) docc.day_3 = type;
            if (sel.day_4 == day) docc.day_4 = type;
            if (sel.day_5 == day) docc.day_5 = type;
            if (sel.day_6 == day) docc.day_6 = type;
            if (sel.day_7 == day) docc.day_7 = type;
            if (sel.day_8 == day) docc.day_8 = type;
            if (sel.day_9 == day) docc.day_9 = type;
            if (sel.day_10 == day) docc.day_10 = type;
            if (sel.day_11 == day) docc.day_11 = type;
            if (sel.day_12 == day) docc.day_12 = type;
            if (sel.day_13 == day) docc.day_13 = type;
            if (sel.day_14 == day) docc.day_14 = type;
            if (sel.day_15 == day) docc.day_15 = type;
            if (sel.day_16 == day) docc.day_16 = type;
            if (sel.day_17 == day) docc.day_17 = type;
            if (sel.day_18 == day) docc.day_18 = type;
            if (sel.day_19 == day) docc.day_19 = type;
            if (sel.day_20 == day) docc.day_20 = type;
            if (sel.day_21 == day) docc.day_21 = type;
            if (sel.day_22 == day) docc.day_22 = type;
            if (sel.day_23 == day) docc.day_23 = type;
            if (sel.day_24 == day) docc.day_24 = type;
            if (sel.day_25 == day) docc.day_25 = type;
            if (sel.day_26 == day) docc.day_26 = type;
            if (sel.day_27 == day) docc.day_27 = type;
            if (sel.day_28 == day) docc.day_28 = type;
            if (sel.day_29 == day) docc.day_29 = type;
            if (sel.day_30 == day) docc.day_30 = type;
            if (sel.day_31 == day) docc.day_31 = type;

            return help;
        }

        private async void MakeMonthUnchangeable (int id) {
            var selectedMonth = await _repo.GetMonth (id); // so this gives the month number of 3 for example
          //  var selectedMonthOccupancy = await _repo.getDateOccupancy (selectedMonth.UserId);
           /*  if (selectedMonthOccupancy.day_1 != 3 || selectedMonthOccupancy.day_10 != 3 || selectedMonthOccupancy.day_20 != 3) { // check to see if this month is already blocked out
                selectedMonthOccupancy.day_1 = changeToUnchangeable(selectedMonthOccupancy.day_1);
                selectedMonthOccupancy.day_2 = changeToUnchangeable(selectedMonthOccupancy.day_2);
                selectedMonthOccupancy.day_3 = changeToUnchangeable(selectedMonthOccupancy.day_3);
                selectedMonthOccupancy.day_4 = changeToUnchangeable(selectedMonthOccupancy.day_4);
                selectedMonthOccupancy.day_5 = changeToUnchangeable(selectedMonthOccupancy.day_5);
                selectedMonthOccupancy.day_6 = changeToUnchangeable(selectedMonthOccupancy.day_6);
                selectedMonthOccupancy.day_7 = changeToUnchangeable(selectedMonthOccupancy.day_7);
                selectedMonthOccupancy.day_8 = changeToUnchangeable(selectedMonthOccupancy.day_8);
                selectedMonthOccupancy.day_9 = changeToUnchangeable(selectedMonthOccupancy.day_9);
                selectedMonthOccupancy.day_10 = changeToUnchangeable(selectedMonthOccupancy.day_10);
                selectedMonthOccupancy.day_11 = changeToUnchangeable(selectedMonthOccupancy.day_11);
                selectedMonthOccupancy.day_12 = changeToUnchangeable(selectedMonthOccupancy.day_12);
                selectedMonthOccupancy.day_13 = changeToUnchangeable(selectedMonthOccupancy.day_13);
                selectedMonthOccupancy.day_14 = changeToUnchangeable(selectedMonthOccupancy.day_14);
                selectedMonthOccupancy.day_15 = changeToUnchangeable(selectedMonthOccupancy.day_15);
                selectedMonthOccupancy.day_16 = changeToUnchangeable(selectedMonthOccupancy.day_16);
                selectedMonthOccupancy.day_17 = changeToUnchangeable(selectedMonthOccupancy.day_17);
                selectedMonthOccupancy.day_18 = changeToUnchangeable(selectedMonthOccupancy.day_18);
                selectedMonthOccupancy.day_19 = changeToUnchangeable(selectedMonthOccupancy.day_19);
                selectedMonthOccupancy.day_20 = changeToUnchangeable(selectedMonthOccupancy.day_20);
                selectedMonthOccupancy.day_21 = changeToUnchangeable(selectedMonthOccupancy.day_21);
                selectedMonthOccupancy.day_22 = changeToUnchangeable(selectedMonthOccupancy.day_22);
                selectedMonthOccupancy.day_23 = changeToUnchangeable(selectedMonthOccupancy.day_23);
                selectedMonthOccupancy.day_24 = changeToUnchangeable(selectedMonthOccupancy.day_24);
                selectedMonthOccupancy.day_25 = changeToUnchangeable(selectedMonthOccupancy.day_25);
                selectedMonthOccupancy.day_26 = changeToUnchangeable(selectedMonthOccupancy.day_26);
                selectedMonthOccupancy.day_27 = changeToUnchangeable(selectedMonthOccupancy.day_27);
                selectedMonthOccupancy.day_28 = changeToUnchangeable(selectedMonthOccupancy.day_28);
                selectedMonthOccupancy.day_29 = changeToUnchangeable(selectedMonthOccupancy.day_29);
                selectedMonthOccupancy.day_30 = changeToUnchangeable(selectedMonthOccupancy.day_30);
                selectedMonthOccupancy.day_31 = changeToUnchangeable(selectedMonthOccupancy.day_31);
                selectedMonthOccupancy.day_32 = changeToUnchangeable(selectedMonthOccupancy.day_32);
                selectedMonthOccupancy.day_33 = changeToUnchangeable(selectedMonthOccupancy.day_33);
                selectedMonthOccupancy.day_34 = changeToUnchangeable(selectedMonthOccupancy.day_34);
                selectedMonthOccupancy.day_35 = changeToUnchangeable(selectedMonthOccupancy.day_35);
                selectedMonthOccupancy.day_36 = changeToUnchangeable(selectedMonthOccupancy.day_36);
                selectedMonthOccupancy.day_37 = changeToUnchangeable(selectedMonthOccupancy.day_37);
                selectedMonthOccupancy.day_38 = changeToUnchangeable(selectedMonthOccupancy.day_38);
                selectedMonthOccupancy.day_39 = changeToUnchangeable(selectedMonthOccupancy.day_39);
                selectedMonthOccupancy.day_40 = changeToUnchangeable(selectedMonthOccupancy.day_40);
                selectedMonthOccupancy.day_41 = changeToUnchangeable(selectedMonthOccupancy.day_41);
                selectedMonthOccupancy.day_42 = changeToUnchangeable(selectedMonthOccupancy.day_42);
                await _repo.SaveAll ();
            */ }

        }

        /* private int changeToUnchangeable(int test)
        {
            // do not erase all the passed appointments, only the vacant ones
            if(test == 0){ return 3;}
            return test;
        } */
    }
