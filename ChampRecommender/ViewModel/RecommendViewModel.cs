using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
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
                string subtier = soloRankTierInfo["division"].ToString();

                await gameStatics.initGameStatics(puuid);

                _summoner = new Summoner
                {
                    AccountId = accountId,
                    SummonerId = summonerId,
                    puuid = puuid,
                    Name = name,
                    Tier = tier,
                    SubTier = subtier == "None" ? -1 : Convert.ToInt32(subtier)
                };
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

        private ChampRecommend? _champRecommend;
        private Summoner? _summoner;

        public RecommendViewModel()
        {
        }

        public async Task initRecommendViewModel()
        {
            await setSummoner();
        }
    }
}
