using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.Models
{
    public static class Tier
    {
        public const string None = "None";
        public const string Iron = "IRON";
        public const string Bronze = "BRONZE";
        public const string Silver = "SILVER";
        public const string Gold = "GOLD";
        public const string Platinum = "PLATINUM";
        public const string Diamond = "DIAMOND";
        public const string Maseter = "MASTER";
        public const string Grandmaster = "GRANDMASTER";
        public const string Challenger = "CHALLENGER";
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
