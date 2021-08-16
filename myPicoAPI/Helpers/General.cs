using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using AutoMapper;
using DatingApp.API.Data;
using myPicoAPI.Dtos;
using myPicoAPI.Models;

namespace myPicoAPI.Helpers {
    public class General {
        private const string V = @"Data/season/season.xml";
        private const string W = @"Data/season/seasonPrice.xml";

        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public General (IDatingRepository repo, IMapper mapper) {

            _mapper = mapper;
            _repo = repo;
        }

        public async Task<string> getCaretakerMobile (int unitId) {
            var a = await _repo.GetPicoUnit (unitId);
            return a.CaretakerMobile;
        }
        public async Task<string> getUserName (int Id) {
            var a = await _repo.GetUser (Id);
            return a.KnownAs;
        }

        public double getPrice (int unit, int dayNumber, int year) {
            var season = getSeasonForThisDay (dayNumber, year);
            return getSeasonPrices (unit, season);
        }

        public async Task<bool> getAlertSent (int apptId) {
            var help = false;
            var a = await _repo.GetAppointment (apptId);
            if (a.BookingAlertSent == 0) { help = true; }
            return help;
        }
        public async Task<string> setAlertSent (int Id) {
            var a = await _repo.GetAppointment (Id);
            a.BookingAlertSent = 1;
            await _repo.SaveAll ();
            return "";
        }

        public dateOccupancy getSeasonForThisMonth (int year, int month) {

            // nog the code for the appt toevoegen
            var help = new dateOccupancy ();

            XDocument xdoc = XDocument.Load (V);
            IEnumerable<XElement> el = from t in xdoc.Descendants ("year")
            where (int) t.Attribute ("id") == year
            select t;

            IEnumerable<XElement> h = from s in el.Descendants ("month")
            where (int) s.Attribute ("id") == month
            select s;

            foreach (XElement j in h) {
                help.Id = year;
                help.Month_ModelId = month;
                help.day_1 = getDailySeason (1, h);
                help.day_2 = getDailySeason (2, h);
                help.day_3 = getDailySeason (3, h);
                help.day_4 = getDailySeason (4, h);
                help.day_5 = getDailySeason (5, h);
                help.day_6 = getDailySeason (6, h);
                help.day_7 = getDailySeason (7, h);
                help.day_8 = getDailySeason (8, h);
                help.day_9 = getDailySeason (9, h);
                help.day_10 = getDailySeason (10, h);
                help.day_11 = getDailySeason (11, h);
                help.day_12 = getDailySeason (12, h);
                help.day_13 = getDailySeason (13, h);
                help.day_14 = getDailySeason (14, h);
                help.day_15 = getDailySeason (15, h);
                help.day_16 = getDailySeason (16, h);
                help.day_17 = getDailySeason (17, h);
                help.day_18 = getDailySeason (18, h);
                help.day_19 = getDailySeason (19, h);
                help.day_20 = getDailySeason (20, h);
                help.day_21 = getDailySeason (21, h);
                help.day_22 = getDailySeason (22, h);
                help.day_23 = getDailySeason (23, h);
                help.day_24 = getDailySeason (24, h);
                help.day_25 = getDailySeason (25, h);
                help.day_26 = getDailySeason (26, h);
                help.day_27 = getDailySeason (27, h);
                help.day_28 = getDailySeason (28, h);
                help.day_29 = getDailySeason (29, h);
                help.day_30 = getDailySeason (30, h);
                help.day_31 = getDailySeason (31, h);
                help.day_32 = getDailySeason (32, h);
                help.day_33 = getDailySeason (33, h);
                help.day_34 = getDailySeason (34, h);
                help.day_35 = getDailySeason (35, h);
                help.day_36 = getDailySeason (36, h);
                help.day_37 = getDailySeason (37, h);
                help.day_38 = getDailySeason (38, h);
                help.day_39 = getDailySeason (39, h);
                help.day_40 = getDailySeason (40, h);
                help.day_41 = getDailySeason (41, h);
                help.day_42 = getDailySeason (42, h);

            }

            return help;
        }

        public int getDailySeason (int testDay, IEnumerable<XElement> h) {
            int help = 0;
            IEnumerable<XElement> b = from s in h.Descendants ("day")
            where (int) s.Attribute ("dn") == testDay
            select s;
            foreach (XElement el in b) {
                help = Convert.ToInt32 (el.Element ("season").Value);
            };
            return help;
        }

        private void saveDailySeason (int testDay, IEnumerable<XElement> h, int newValue) {
            IEnumerable<XElement> b = from s in h.Descendants ("day")
            where (int) s.Attribute ("dn") == testDay
            select s;
            foreach (XElement el in b) {
                el.SetElementValue ("season", newValue);
            };
        }

        public int getSeasonForThisDay (int dayNumber, int year) {
            var help = 0;
            DateTime theDate = new DateTime (year, 1, 1).AddDays (dayNumber - 1);
            var selectedDay = theDate.Day;
            var selectedMonth = theDate.Month;

            XDocument xdoc = XDocument.Load (V);

            IEnumerable<XElement> el = from t in xdoc.Descendants ("year")
            where (int) t.Attribute ("id") == year
            select t;

            IEnumerable<XElement> h = from s in el.Descendants ("month")
            where (int) s.Attribute ("id") == selectedMonth
            select s;
            help = getDailySeason (selectedDay, h);

            return help;
        }

        public void saveSeasonPrice (int newPrice, string picoUnitId, string seasonType) {
            XDocument xdoc = XDocument.Load (W);
            IEnumerable<XElement> el = from t in xdoc.Descendants ("unit")
            where (string) t.Attribute ("id") == picoUnitId
            select t;
            foreach (XElement j in el) {
                if (seasonType == "6") { j.SetElementValue ("priceLowSeason", newPrice); }
                if (seasonType == "7") { j.SetElementValue ("priceMidSeason", newPrice); }
                if (seasonType == "8") { j.SetElementValue ("priceHighSeason", newPrice); }
            }
            xdoc.Save (W);
        }

        public void saveSeason (SeasonForReturnDTO sr) {

            var help = _mapper.Map<dateOccupancy> (sr);

            XDocument xdoc = XDocument.Load (V);
            IEnumerable<XElement> el = from t in xdoc.Descendants ("year")
            where (int) t.Attribute ("id") == help.Id
            select t;

            IEnumerable<XElement> h = from s in el.Descendants ("month")
            where (int) s.Attribute ("id") == help.Month_ModelId
            select s;

            foreach (XElement j in h) {
                saveDailySeason (1, h, help.day_1);
                saveDailySeason (2, h, help.day_2);
                saveDailySeason (3, h, help.day_3);
                saveDailySeason (4, h, help.day_4);
                saveDailySeason (5, h, help.day_5);
                saveDailySeason (6, h, help.day_6);
                saveDailySeason (7, h, help.day_7);
                saveDailySeason (8, h, help.day_8);
                saveDailySeason (9, h, help.day_9);
                saveDailySeason (10, h, help.day_10);
                saveDailySeason (11, h, help.day_11);
                saveDailySeason (12, h, help.day_12);
                saveDailySeason (13, h, help.day_13);
                saveDailySeason (14, h, help.day_14);
                saveDailySeason (15, h, help.day_15);
                saveDailySeason (16, h, help.day_16);
                saveDailySeason (17, h, help.day_17);
                saveDailySeason (18, h, help.day_18);
                saveDailySeason (19, h, help.day_19);
                saveDailySeason (20, h, help.day_20);
                saveDailySeason (21, h, help.day_21);
                saveDailySeason (22, h, help.day_22);
                saveDailySeason (23, h, help.day_23);
                saveDailySeason (24, h, help.day_24);
                saveDailySeason (25, h, help.day_25);
                saveDailySeason (26, h, help.day_26);
                saveDailySeason (27, h, help.day_27);
                saveDailySeason (28, h, help.day_28);
                saveDailySeason (29, h, help.day_29);
                saveDailySeason (30, h, help.day_30);
                saveDailySeason (31, h, help.day_31);
                saveDailySeason (32, h, help.day_32);
                saveDailySeason (33, h, help.day_33);
                saveDailySeason (34, h, help.day_34);
                saveDailySeason (35, h, help.day_35);
                saveDailySeason (36, h, help.day_36);
                saveDailySeason (37, h, help.day_37);
                saveDailySeason (38, h, help.day_38);
                saveDailySeason (39, h, help.day_39);
                saveDailySeason (40, h, help.day_40);
                saveDailySeason (41, h, help.day_41);
                saveDailySeason (42, h, help.day_42);
                
               
            }
            xdoc.Save (V);
        }

        public double getSeasonPrices (int picoUnit, int seasonType) {
            var help = 0.0;
            XDocument xdoc = XDocument.Load (W);
            IEnumerable<XElement> el = from t in xdoc.Descendants ("unit")
            where (int) t.Attribute ("id") == picoUnit
            select t;
            foreach (XElement j in el) {
                if (seasonType == 6) { help = Convert.ToDouble (j.Element ("priceLowSeason").Value); }
                if (seasonType == 7) { help = Convert.ToDouble (j.Element ("priceMidSeason").Value); }
                if (seasonType == 8) { help = Convert.ToDouble (j.Element ("priceHighSeason").Value); }
            }
            return help;
        }
    }
}