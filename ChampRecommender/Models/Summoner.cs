﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.Models
{
    class Summoner
    {
        public string? AccountId { get; set; }

        public string? SummonerId { get; set; }

        public string? puuid { get; set; }

        public string? Name { get; set; }    

        public string? Tier { get; set; }

        public int? SubTier { get; set; }
    }
}
