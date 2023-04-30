using ChampRecommender.Dataset;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections;
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

        public const string CHAMP_SELECT_SESSION = "/lol-champ-select/v1/session";

        public const string ACS_CURRENT_USER = "/lol-acs/v2/recently-played-champions/current-summoner";

        public static string CURRENT_SUMMONER_TIER(string puuid)
        {
            return String.Format("/lol-ranked/v1/ranked-stats/{0}", puuid);
        }

        public static string INVENTORIES_CHAMPIONS(string summonerId)
        {
            return String.Format("/lol-champions/v1/inventories/{0}/champions", summonerId);
        }

        public static string CAREER_STATS(string puuid, string season, string queue, string position)
        {
            return String.Format("/lol-career-stats/v1/summoner-stats/{0}/{1}/{2}/{3}", puuid, season, queue, position);
        }

        public static string CHAMP_PLAY_COUNT(string summonerId)
        {
            return String.Format("/lol-champions/v1/inventories/{0}/champions-playable-count", summonerId);
        }

        public static string CHAMP_MASTERY(string summonerId)
        {
            return String.Format("/lol-collections/v1/inventories/{0}/champion-mastery", summonerId);
        }

        public static string CHAMP_MASTERY_TOP(string summonerId)
        {
            return String.Format("/lol-collections/v1/inventories/{0}/champion-mastery/top", summonerId);
        }

        public static string CAREER_STATS_SUMMONER(string puuid)
        {
            return String.Format("/lol-career-stats/v1/summoner-games/{0}", puuid);
        }
    }
}
