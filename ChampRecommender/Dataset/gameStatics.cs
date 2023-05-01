using ChampRecommender.Models;
using ChampRecommender.ViewModel;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools.V110.Network;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.Dataset
{
    public static class gameStatics
    {
        private const string rankGame = "rank5solo";
        
        static private int gameCount;

        static private int season;

        static private List<ChampionStatics> champStats;

        static gameStatics() { }

        public static async Task initGameStatics(string puuid) 
        {
            JArray games = await RiotCLUManager.UsingApiEventJArray(APIMethod.GET, APIEndpoint.CAREER_STATS_SUMMONER(puuid));
            
            List<List<JObject>> classifiedGames = new List<List<JObject>>();
            List<int> checkedChampions = new List<int>();

            classifiedGames.Add(new List<JObject>());
            checkedChampions.Add(0);

            foreach (var game in games)
            {
                if (game["queueType"].ToString() == rankGame)
                {
                    gameCount++;
                    season = (int)game["season"];
                    int champId = (int)game["championId"];
                    List<JObject> curList;
                    if (!checkedChampions.Contains(champId))
                    {
                        checkedChampions.Add(champId);
                        curList = new List<JObject>();
                        classifiedGames.Add(curList);
                    }
                    else
                    {
                        curList = classifiedGames[checkedChampions.IndexOf(champId)];
                    }
                    curList.Add(game.ToObject<JObject>());
                }
                else
                {
                    classifiedGames[0].Add(game.ToObject<JObject>());
                }
            }
            int lenList = checkedChampions.Count;
            champStats = new List<ChampionStatics>(lenList);
            for (int i = 1; i < lenList; i++)
            {
                champStats.Add(new ChampionStatics(classifiedGames[i], checkedChampions[i]));
            }
            champStats.Sort(new Comparison<ChampionStatics>((c1, c2) => c2.getCount().CompareTo(c1.getCount())));

            Console.Write(champStats.ToString());
        }

        public static List<ChampionStatics> GetChampionStatics() { return champStats; }
    }

    public class ChampionStatics
    {
        private int gameCount;

        private Champion? champion;

        private int winCount;

        private int winRate;

        public ChampionStatics(List<JObject> classifiedGames, int id) 
        {
            champion = Champions.GetChampionById(id);
            gameCount = classifiedGames.Count;
            foreach (JObject game in classifiedGames)
            {
                if ((int)game["stats"]["CareerStats.js"]["victory"] != 0)
                {
                    winCount++;
                }
            }
            winRate = (winCount * 100) / gameCount;
        }

        public int getCount() { return gameCount; }

        public int getWinRate() {  return winRate; }

        public Champion? GetChampion() { return champion; }
    }
}
