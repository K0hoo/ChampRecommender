﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampRecommender.Dataset;
using ChampRecommender.Models;
using ChampRecommender.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChampRecommender.ViewModel
{
    internal class RecommendViewModel : BaseViewModel
    {

        public Summoner? getSummoner() { return _summoner; }

        public async void setSummoner()
        {
            try
            {
                JObject? summonerJson = null;
                while (true)
                {
                    summonerJson = await RiotCLUManager.UsingApiEventJObject(APIMethod.GET, APIEndpoint.CURRENT_SUMMONER);
                    if (summonerJson != null) break;
                    await Task.Delay(1000 * 10);
                }
                string accountId = summonerJson["accountId"].ToString();
                string summonerId = summonerJson["summonerId"].ToString();
                string puuid = summonerJson["puuid"].ToString();
                string name = summonerJson["displayName"].ToString();

                JObject tierInfo = await RiotCLUManager.UsingApiEventJObject(APIMethod.GET, APIEndpoint.CURRENT_SUMMONER_TIER(puuid));
                JToken soloRankTierInfo = tierInfo["queueMap"]["RANKED_SOLO_5x5"];
                string tier = soloRankTierInfo["tier"].ToString();
                string subtier = soloRankTierInfo["division"].ToString();

                gameStatics.initGameStatics(puuid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }

        public ChampRecommend champRecommend
        {
            get => _champRecommend;
            set
            {
                if (_champRecommend != value)
                {
                    _champRecommend = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ChampRecommend _champRecommend = new ChampRecommend();
        private Summoner? _summoner;

        public RecommendViewModel()
        {
            setSummoner();
        }
    }
}
