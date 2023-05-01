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
                if (subtierRome == "NA") { subtierRome = soloRankTierInfo["leaguePoints"].ToString(); }
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
                        subtier = Convert.ToInt32(subtierRome);
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
        
        public string? summonerName
        {
            get => _summonerName;
            set
            {
                if (_summonerName != value)
                {
                    _summonerName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? winRate
        {
            get => _winRate;
            set
            {
                if (_winRate != value)
                {
                    _winRate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? tier
        {
            get => _tier;
            set
            {
                if (_tier != value)
                {
                    _tier = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? mostLaneImage
        {
            get => _mostLaneImage;
            set
            {
                if (_mostLaneImage != value)
                {
                    _mostLaneImage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? mostLane
        {
            get => _mostLane;
            set
            {
                if (_mostLane != value)
                {
                    _mostLane = value;
                    NotifyPropertyChanged();
                }
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

        public string? champ0WinRate
        {
            get => _champ0WinRate;
            set
            {
                if (_champ0WinRate != value)
                {
                    _champ0WinRate = value + "%";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ1WinRate
        {
            get => _champ1WinRate;
            set
            {
                if (_champ1WinRate != value)
                {
                    _champ1WinRate = value + "%";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ2WinRate
        {
            get => _champ2WinRate;
            set
            {
                if (_champ2WinRate != value)
                {
                    _champ2WinRate = value + "%";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ3WinRate
        {
            get => _champ3WinRate;
            set
            {
                if (_champ3WinRate != value)
                {
                    _champ3WinRate = value + "%";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ4WinRate
        {
            get => _champ4WinRate;
            set
            {
                if (_champ4WinRate != value)
                {
                    _champ4WinRate = value + "%";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ0Played
        {
            get => _champ0Played;
            set
            {
                if (_champ0Played != value)
                {
                    _champ0Played = value + " played";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ1Played
        {
            get => _champ1Played;
            set
            {
                if (_champ1Played != value)
                {
                    _champ1Played = value + " played";
                    NotifyPropertyChanged();
                }
            }
        }
        public string? champ2Played
        {
            get => _champ2Played;
            set
            {
                if (_champ2Played != value)
                {
                    _champ2Played = value + " played";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ3Played
        {
            get => _champ3Played;
            set
            {
                if (_champ3Played != value)
                {
                    _champ3Played = value + " played";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ4Played
        {
            get => _champ4Played;
            set
            {
                if (_champ4Played != value)
                {
                    _champ4Played = value + " played";
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ0Image
        {
            get => _champ0Image;
            set
            {
                if (_champ0Image != value)
                {
                    _champ0Image = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ1Image
        {
            get => _champ1Image;
            set
            {
                if (_champ1Image != value)
                {
                    _champ1Image = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ2Image
        {
            get => _champ2Image;
            set
            {
                if (_champ2Image != value)
                {
                    _champ2Image = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ3Image
        {
            get => _champ3Image;
            set
            {
                if (_champ3Image != value)
                {
                    _champ3Image = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? champ4Image
        {
            get => _champ4Image;
            set
            {
                if (_champ4Image != value)
                {
                    _champ4Image = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Summoner? _summoner;
        private List<ChampionStatics>? championStatics;

        private string _summonerName = "";
        private string _winRate = "";
        private string _tier = "";
        private string _mostLaneImage = "";
        private string _mostLane = "";

        private string _champ0Name = "";
        private string _champ1Name = "";
        private string _champ2Name = "";
        private string _champ3Name = "";
        private string _champ4Name = "";

        private string _champ0WinRate = "";
        private string _champ1WinRate = "";
        private string _champ2WinRate = "";
        private string _champ3WinRate = "";
        private string _champ4WinRate = "";

        private string _champ0Played = "";
        private string _champ1Played = "";
        private string _champ2Played = "";
        private string _champ3Played = "";
        private string _champ4Played = "";

        private string _champ0Image = "";
        private string _champ1Image = "";
        private string _champ2Image = "";
        private string _champ3Image = "";
        private string _champ4Image = "";

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
                championStatics = gameStatics.GetChampionStatics();

                summonerName = getSummoner().Name;
                winRate = String.Format("Win Rate: {0}% ({1}/{2})", 
                    gameStatics.GetWinRate().ToString(),
                    gameStatics.GetWinCount().ToString(),
                    gameStatics.GetGameCount().ToString()
                    );
                tier = String.Format("{0} {1}", getSummoner().Tier, getSummoner().SubTier);

                mostLaneImage = String.Format("/Windows/l_{0}.png", gameStatics.GetMostLane());
                mostLane = gameStatics.GetMostLane();

                champ0Name = championStatics[0].GetChampion().Name;
                champ1Name = championStatics[1].GetChampion().Name;
                champ2Name = championStatics[2].GetChampion().Name;
                champ3Name = championStatics[3].GetChampion().Name;
                champ4Name = championStatics[4].GetChampion().Name;

                champ0WinRate = championStatics[0].getWinRate().ToString();
                champ1WinRate = championStatics[1].getWinRate().ToString();
                champ2WinRate = championStatics[2].getWinRate().ToString();
                champ3WinRate = championStatics[3].getWinRate().ToString();
                champ4WinRate = championStatics[4].getWinRate().ToString();

                champ0Played = championStatics[0].getCount().ToString();
                champ1Played = championStatics[1].getCount().ToString();
                champ2Played = championStatics[2].getCount().ToString();
                champ3Played = championStatics[3].getCount().ToString();
                champ4Played = championStatics[4].getCount().ToString();

                champ0Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ0Name);
                champ1Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ1Name);
                champ2Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ2Name);
                champ3Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ3Name);
                champ4Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ4Name);
            }
        }
    }
}
