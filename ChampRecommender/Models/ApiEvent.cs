using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.Models
{
    static class APIMethod
    {
        public const string GET = "Get";

        public const string POST = "Post";

        public const string PUT = "Put";

        public const string DELETE = "Delete";
    }

    static class APIEndpoint
    {
        public const string CURRENT_SUMMONER = "/lol-summoner/v1/current-summoner";

        public static string CURRENT_SUMMONER_TIER(string puuid)
        {
            return "/lol-ranked/v1/ranked-stats/" + puuid;
        }
    }
}
