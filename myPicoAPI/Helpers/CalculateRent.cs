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
        public double getRentPHP (string requestedDays, int year, int PicoId) {
            var pricedDays = new List<string> ();
            var total_price = 0.0;
            var gen = new General (_repo, _mapper);
            string[] dna = requestedDays.Split (',');
            for (int i = 0; i < dna.Length; i++) { total_price = total_price + gen.getPrice (PicoId, Convert.ToInt32(dna[i]), year); }
            return total_price;
        }

        public double getRentUSD (string requestedDays, int year, int PicoId) {
            var conversionToUSD = 0.15;
            var total_price = 0.0;
            total_price = getRentPHP (requestedDays, year, PicoId);
            total_price = total_price * conversionToUSD;
            return total_price;
        }

    }
}