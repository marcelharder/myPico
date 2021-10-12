using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AutoMapper;
using DatingApp.API.Data;

namespace myPicoAPI.Helpers {
    public class CalculateRent {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public CalculateRent (IDatingRepository repo, IMapper mapper) {
            _mapper = mapper;
            _repo = repo;
        }
        public double getRentPHP (string[] requestedDays, int year, int PicoId) {
            var pricedDays = new List<string> ();
            var total_price = 0.0;
            var gen = new General (_repo, _mapper);
            for (int i = 0; i < requestedDays.Length; i++) { total_price = total_price + gen.getPrice (PicoId, Convert.ToInt32(requestedDays[i]), year); }
            return total_price;
        }

        

    }
}