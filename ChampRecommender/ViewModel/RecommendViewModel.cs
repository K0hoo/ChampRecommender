using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using ChampRecommender.Dataset;
using ChampRecommender.Models;
using ChampRecommender.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace ChampRecommender.ViewModel
{
    internal class RecommendViewModel : BaseViewModel
    {

        public Summoner? getSummoner() { return _summoner; }

        public async Task setSummoner()
        {
            try
            {
                JObject? summonerJson;
                while (true)
                {
                    summonerJson = await RiotCLUManager.UsingApiEventJObject(APIMethod.GET, APIEndpoint.CURRENT_SUMMONER);
                    if (summonerJson != null && summonerJson.ContainsKey("accountId")) break;
                    await Task.Delay(1000);
                }
                string accountId = summonerJson["accountId"].ToString();
                string summonerId = summonerJson["summonerId"].ToString();
                string puuid = summonerJson["puuid"].ToString();
                string name = summonerJson["displayName"].ToString();

                JObject tierInfo = await RiotCLUManager.UsingApiEventJObject(APIMethod.GET, APIEndpoint.CURRENT_SUMMONER_TIER(puuid));
                JToken soloRankTierInfo = tierInfo["queueMap"]["RANKED_SOLO_5x5"];
                string tier = soloRankTierInfo["tier"].ToString();
                string subtierRome = soloRankTierInfo["division"].ToString();
                int subtier;

                switch (subtierRome)
                {
                    case "I":
                        subtier = 1;
                        break;
                    case "II":
                        subtier = 2;
                        break;
                    case "III":
                        subtier = 3;
                        break;
                    case "IV":
                        subtier = 4;
                        break;
                    default:
                        subtier = -1;
                        break;
                }

                await gameStatics.initGameStatics(puuid);

                _summoner = new Summoner
                {
                    AccountId = accountId,
                    SummonerId = summonerId,
                    puuid = puuid,
                    Name = name,
                    Tier = tier,
                    SubTier = subtier
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }

        public string? champ0Name
        {
            get => _champ0Name;
            set
            {
                if (_champ0Name != value)
                {
                    _champ0Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ1Name
        {
            get => _champ1Name;
            set
            {
                if (_champ1Name != value)
                {
                    _champ1Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ2Name
        {
            get => _champ2Name;
            set
            {
                if (_champ2Name != value)
                {
                    _champ2Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ3Name
        {
            get => _champ3Name;
            set
            {
                if (_champ3Name != value)
                {
                    _champ3Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ4Name
        {
            get => _champ4Name;
            set
            {
                if (_champ4Name != value)
                {
                    _champ4Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Summoner? _summoner;

        private string _champ0Name = "-";
        private string _champ1Name = "-";
        private string _champ2Name = "-";
        private string _champ3Name = "-";
        private string _champ4Name = "-";


        public RecommendViewModel()
        {
        }

        public async Task initRecommendViewModel()
        {
            await setSummoner();
        }

        public async Task SetMostChampion()
        {
            if (_summoner != null) 
            {
                List<ChampionStatics>? championStatics = gameStatics.GetChampionStatics();
                champ0Name = championStatics[0].GetChampion().Name;
                champ1Name = championStatics[1].GetChampion().Name;
                champ2Name = championStatics[2].GetChampion().Name;
                champ3Name = championStatics[3].GetChampion().Name;
                champ4Name = championStatics[4].GetChampion().Name;
            }
        }
    }
}
