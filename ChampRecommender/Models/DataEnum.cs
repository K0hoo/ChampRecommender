using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.Models
{
    public enum Tier
    {
        None,
        IRON,
        BRONZE,
        SILVER,
        GOLD,
        PLATINUM,
        DIAMOND,
        MASTER,
        GRANDMASTER,
        CHALLENGER
    }
    
    public static class Lane
    {
        public const string TOP = "TOP";
        public const string JUNGLE = "JUNGLE";
        public const string MID = "MID";
        public const string BOTTOM = "BOTTOM";
        public const string SUPPORT = "SUPPORT";
    }

    public enum LaneE
    {
        TOP, JUNGLE, MID, BOTTOM, SUPPORT
    }
}
