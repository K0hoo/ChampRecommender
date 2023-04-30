using ChampRecommender.Models;
using ChampRecommender.ViewModel;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools.V110.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.Dataset
{
    public static class gameStatics
    {
        private const string rankGame = "rank5solo";
        
        static private int gameCount;

        static private int season;



        static gameStatics() { }

        public static async void initGameStatics(string puuid) 
        {
            JArray games = await RiotCLUManager.UsingApiEventJArray(APIMethod.GET, APIEndpoint.CAREER_STATS_SUMMONER(puuid));
            
            List<List<JObject>> classifiedGames = new List<List<JObject>>();
            List<int> CheckedChampions = new List<int>();

            classifiedGames.Add(new List<JObject>());

            foreach (var game in games)
            {
                if (game["queueType"].ToString() == rankGame)
                {
                    season = (int)game["season"];

                }
                else
                {
                    classifiedGames[0].Add(game.ToObject<JObject>());
                }
            }
        }
    }

    public class ChampionStatics
    {
        private int gameCount;

        private Champion? champion;

        public ChampionStatics(int id) 
        {
            Champion? champion = Champions.GetChampionById(id);

        }
    }
}
