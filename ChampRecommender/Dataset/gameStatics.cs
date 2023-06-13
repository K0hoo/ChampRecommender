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

        static private int winCount;

        static private int winRate;

        static private int season;

        static private string? mostLane = "None";

        static private List<ChampionStatics> champStats;

        static gameStatics() { }

        public static async Task initGameStatics(string puuid) 
        {
            JArray games = await RiotCLUManager.UsingApiEventJArray(APIMethod.GET, APIEndpoint.CAREER_STATS_SUMMONER(puuid));
            if (games == null) return;

            List<List<JObject>> classifiedGames = new List<List<JObject>>();
            List<int> checkedChampions = new List<int>();

            classifiedGames.Add(new List<JObject>());
            checkedChampions.Add(0);

            int[] laneCount = { 0, 0, 0, 0, 0};

            foreach (var game in games)
            {
                if (game["queueType"].ToString() == rankGame)
                {
                    gameCount++;

                    if ((int)game["stats"]["CareerStats.js"]["victory"] != 0)
                    {
                        winCount++;
                    }

                    switch (game["lane"].ToString())
                    {
                        case "TOP":
                            laneCount[0]++;
                            break;
                        case "JUNGLE":
                            laneCount[1]++;
                            break;
                        case "MID":
                            laneCount[2]++;
                            break;
                        case "BOTTOM":
                            laneCount[3]++;
                            break;
                        case "SUPPORT":
                            laneCount[4]++;
                            break;
                        default:
                            break;
                    }

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

            int laneMax = laneCount.Max();
            int laneIdx = Array.IndexOf(laneCount, laneMax);

            switch (laneIdx)
            {
                case 0:
                    mostLane = Lane.TOP; break;
                case 1:
                    mostLane = Lane.JUNGLE; break;
                case 2:
                    mostLane = Lane.MID; break; 
                case 3:
                    mostLane = Lane.BOTTOM; break;
                case 4:
                    mostLane = Lane.SUPPORT; break;
                default: break;
            }

            winRate = gameCount!=0 ? winCount * 100 / gameCount : 0;

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

        public static int GetWinRate() { return winRate; }

        public static int GetGameCount() { return gameCount; }

        public static int GetWinCount() {  return winCount; }

        public static string? GetMostLane() { return mostLane; }
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
