using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.Models
{
    class Summoner
    {
        public string accountId { get; set; }

        public string summonerId { get; set; }

        public string puuid { get; set; }

        public string Name { get; set; }    

        public Lane MainLane { get; set; }

        public List<Champion> Availablechamp { get; set; }

        public Tier Tier { get; set; }

        public int SubTier { get; set; }
    }
}
