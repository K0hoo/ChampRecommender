using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ChampRecommender.Dataset;
using ChampRecommender.Models;
using ChampRecommender.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V110.Debugger;
using OpenQA.Selenium.DevTools.V110.Network;
using OpenQA.Selenium.DevTools.V110.Storage;

namespace ChampRecommender.ViewModel
{
    internal class RecommendViewModel : BaseViewModel
    {
        public HttpClient? httpServer { get; set; }

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
                // Debug.Assert(summonerJson != null);
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
                    if (value == "") _champ0WinRate = value;
                    else _champ0WinRate = value + "%";
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
                    if (value == "") _champ1WinRate = value;
                    else _champ1WinRate = value + "%";
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
                    if (value == "") _champ2WinRate = value;
                    else _champ2WinRate = value + "%";
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
                    if (value == "") _champ3WinRate = value;
                    else _champ3WinRate = value + "%";
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
                    if (value == "") _champ4WinRate = value;
                    else _champ4WinRate = value + "%";
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
                    if (value == "") _champ0Played = value;
                    else _champ0Played = value + " played";
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
                    if (value == "") _champ1Played = value;
                    else _champ1Played = value + " played";
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
                    if (value == "") _champ2Played = value;
                    else _champ2Played = value + " played";
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
                    if (value == "") _champ3Played = value;
                    else _champ3Played = value + " played";
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
                    if (value == "") _champ4Played = value;
                    else _champ4Played = value + " played";
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

        public string? champImage
        {
            get => _champImage;
            set
            {
                if (_champImage != value)
                {
                    _champImage = value;
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

        private string _champImage = "";

        public RecommendViewModel()
        {
        }

        public async Task initRecommendViewModel()
        {
            bool revisited = false;
            await setSummoner();
            while (true)
            {
                await setMostChampion(revisited); 
                revisited = true;
                int pickChampID = await checkBanPick();
                await selectDone(pickChampID);
                if (acceptRecommendation) await sendGameResult(pickChampID);
            }
        }

        private async Task setMostChampion(bool revisited)
        {
            if (_summoner != null) 
            {
                if (revisited) await gameStatics.initGameStatics(_summoner.puuid);

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
                
                if (championStatics.Count > 0)
                {
                    champ0Name = championStatics[0].GetChampion().Name;
                    champ0WinRate = championStatics[0].getWinRate().ToString();
                    champ0Played = championStatics[0].getCount().ToString();
                    champ0Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ0Name);
                }

                if (championStatics.Count > 1)
                {
                    champ1Name = championStatics[1].GetChampion().Name;
                    champ1WinRate = championStatics[1].getWinRate().ToString();
                    champ1Played = championStatics[1].getCount().ToString();
                    champ1Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ1Name);
                }

                if (championStatics.Count > 2)
                {
                    champ2Name = championStatics[2].GetChampion().Name;
                    champ2WinRate = championStatics[2].getWinRate().ToString();
                    champ2Played = championStatics[2].getCount().ToString();
                    champ2Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ2Name);
                }

                if (championStatics.Count > 3)
                {
                    champ3Name = championStatics[3].GetChampion().Name;
                    champ3WinRate = championStatics[3].getWinRate().ToString();
                    champ3Played = championStatics[3].getCount().ToString();
                    champ3Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ3Name);
                }

                if (championStatics.Count > 4)
                {
                    champ4Name = championStatics[4].GetChampion().Name;
                    champ4WinRate = championStatics[4].getWinRate().ToString();
                    champ4Played = championStatics[4].getCount().ToString();
                    champ4Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ4Name);
                }
            }
        }

        private static bool acceptRecommendation = false;
        private static Int64 gameId;

        private async Task<int> checkBanPick()
        {
            try
            {
                JObject? banPickInfo = null;
                JObject currentChamp = JObject.Parse(@"{
                    'line': 4, // top: 1, jungle: 2, mid: 3, bottom: 4, utility: 5
                    'myTeam': {
                        'top': 0,
                        'jungle': 0,
                        'middle': 0,
                        'bottom': 0,
                        'utility': 0
                    },
                    'theirTeam': [0, 0, 0, 0, 0],
                    'ban': [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                    'most_champion': [],
                    'possible_champion': []
                }");
                JArray? recommendation = null;

                // most champions
                JArray most = new JArray();

                foreach (var champion in championStatics)
                {
                    JObject mostchamp = new JObject();
                    mostchamp.Add(new JProperty("id", champion.GetChampion().Id));
                    mostchamp.Add(new JProperty("games", champion.getCount()));
                    mostchamp.Add(new JProperty("winrate", champion.getWinRate()));
                    most.Add(mostchamp);
                }

                currentChamp["most_champion"] = most;

                // possible champions
                JArray? pickableChampion = await RiotCLUManager.UsingApiEventJArray(APIMethod.GET, APIEndpoint.PICKABLE_CHAMP_IDS);
                if (pickableChampion != null) currentChamp["possible_champion"] = pickableChampion;

                // pick order - [[0], [5, 6], [1, 2], [7, 8], [3, 4], [9]] maybe(?)

                bool firstLoaded = true;
                bool banComplete = false;
                int userCellID = 0;
                string[] laneByID = { "", "", "", "", "", "", "", "", "", "" }; // cell id 별 포지션 우리팀 top 1, jug 2, mid 3, bot 4, sup 5, 적팀 순서대로 6 7 8 9 10

                /* For Test
                string path = Directory.GetCurrentDirectory();
                path = Path.Join(path, "../../../../Dataset/banpick_info_example.json");

                using (StreamReader file = File.OpenText(path))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    banPickInfo = (JObject)JToken.ReadFrom(reader);
                } */

                while (true)
                {
                    banPickInfo = await RiotCLUManager.UsingApiEventJObject(APIMethod.GET, APIEndpoint.CHAMP_SELECT_SESSION);
                    if (banPickInfo != null && !banPickInfo.ContainsKey("errorCode"))
                    {
                        JArray? actions = (JArray?)banPickInfo["actions"];
                        if (actions == null) continue;

                        JArray? banPhase = (JArray?)actions[0];
                        JArray? pickPhase = new JArray();
                        if (banPhase == null) continue;

                        int pickidx = 0;
                        foreach (JArray pp in actions)
                        {
                            foreach (JObject p in pp)
                            {
                                if (p["type"].ToString() == "pick") pickPhase.Add(p);
                            }
                        }

                        if (firstLoaded)
                        {
                            JArray? myTeam = (JArray?)banPickInfo["myTeam"];
                            if (myTeam == null) continue;

                            foreach (var cell in myTeam)
                            {
                                int cellId = Convert.ToInt32(cell["cellId"]);
                                string laneStr = cell["assignedPosition"].ToString();

                                laneByID[cellId] = laneStr;

                                if (cell["puuid"].ToString() != "")
                                {
                                    if (laneStr == "top") currentChamp["line"] = 1;
                                    else if (laneStr == "jungle") currentChamp["line"] = 2;
                                    else if (laneStr == "middle") currentChamp["line"] = 3;
                                    else if (laneStr == "bottom") currentChamp["line"] = 4;
                                    else if (laneStr == "utility") currentChamp["line"] = 5;
                                    else currentChamp["line"] = 0;
                                    userCellID = cellId;
                                }
                            }
                            gameId = Convert.ToInt64(banPickInfo["gameId"]);

                            firstLoaded = false;
                        }
                        
                        // Set ban phase.
                        if (!banComplete)
                        {
                            bool done = true;
                            foreach (var item in banPhase.Select((value, index) => (value, index)))
                            {
                                var value = item.value;
                                var index = item.index;

                                if (value["completed"].ToString() == "True")
                                {
                                    currentChamp["ban"][index] = value["championId"].ToString();
                                }
                                else
                                {
                                    done = false;
                                }
                            }
                            if (done) banComplete = true;
                        }

                        // Set pick phase.
                        bool changed = false;

                        foreach (var cell in pickPhase)
                        {
                            int cellid =  Convert.ToInt32(cell["actorCellId"]);

                            // If user completes selecting champion, it is done.
                            if (cellid == userCellID && cell["completed"].ToString() == "True")
                            {
                                if (recommendation != null && recommendation.Contains(cell["championId"].ToString())) 
                                    acceptRecommendation = true;
                                return Convert.ToInt32(cell["championId"].ToString());
                            }

                            string lane = laneByID[cellid];
                            if (lane != "")
                            {
                                int curChamp = Convert.ToInt32(currentChamp["myTeam"][lane].ToString());
                                int newChamp = Convert.ToInt32(cell["championId"].ToString());
                                if (curChamp != newChamp && cell["completed"].ToString() == "True")
                                {
                                    currentChamp["myTeam"][lane] = newChamp;
                                    changed = true;
                                }
                            }
                            else
                            {
                                int curChamp = Convert.ToInt32(((JArray?)currentChamp["theirTeam"])[cellid > 4 ? cellid - 5 : cellid]);
                                int newChamp = Convert.ToInt32(cell["championId"].ToString());
                                if (curChamp != newChamp && cell["completed"].ToString() == "True")
                                {
                                    currentChamp["theirTeam"][cellid > 4 ? cellid - 5 : cellid] = newChamp;
                                    changed = true;
                                }
                            }
                        }

                        if (changed)
                        {
                            Trace.WriteLine(currentChamp.ToString());
                            recommendation = await ServerManager.getRecommendation(currentChamp);
                            await setRecommendation(recommendation);
                        }
                    }
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }

            return 17;
        }

        private async Task setRecommendation(JArray recommendations)
        {
            // JArray JpickableChamp = await RiotCLUManager.UsingApiEventJArray(APIMethod.GET, APIEndpoint.PICKABLE_CHAMP_IDS);
            // List<int> pickableChamp = JpickableChamp.ToObject<List<int>>();
            Trace.WriteLine(recommendations.ToString());
            int index = 0;
            foreach(var recommendation in recommendations)
            {
                // int champId = Convert.ToInt32(recommendation["id"]);
                // if (!pickableChamp.Contains(champId)) continue;
                int champId = Convert.ToInt32(recommendation);
                Champion? champ = Champions.GetChampionById(champId);
                if (champ != null)
                {
                    if (index == 0)
                    {
                        champ0Name = champ.Name;
                        champ0WinRate = "";
                        champ0Played = "";
                        champ0Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ0Name);
                    } else if (index == 1) {
                        champ1Name = champ.Name;
                        champ1WinRate = "";
                        champ1Played = "";
                        champ1Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ1Name);
                    } else if (index == 2) {
                        champ2Name = champ.Name;
                        champ2WinRate = "";
                        champ2Played = "";
                        champ2Image = String.Format("/Windows/crop_champion/{0}_0.jpg", champ2Name);
                    } else if (index == 3) {
                        champ3Name = champ.Name;
                        champ3WinRate = "";
                        champ3Played = "";
                        champ3Image = String.Format("/Windows/_crop_champion/{0}_0.jpg", champ3Name);
                    } else if (index == 4) {
                        champ4Name = champ.Name;
                        champ4WinRate = "";
                        champ4Played = "";
                        champ4Image = String.Format("/Windows/_crop_champion/{0}_0.jpg", champ4Name);
                    }
                    index++;
                }
            }
        }

        private async Task selectDone(int pickChampID)
        {
            Champion? pickChamp = Champions.GetChampionById(pickChampID);
            ChampionStatics? pickChampState = null;
            foreach (ChampionStatics champ in championStatics)
            {
                if (pickChampID == champ.GetChampion().Id)
                {
                    pickChampState = champ;
                    break;
                }
            }

            if (pickChamp == null) return;

            champ0Name = "";
            champ1Name = "";
            champ2Name = "";
            champ3Name = "";
            champ4Name = pickChamp.Name;

            champ0Played = "";
            champ1Played = "";
            champ2Played = "";
            champ3Played = "";
            champ4Played = pickChampState != null ? pickChampState.getCount().ToString() : "";

            champ0WinRate = "";
            champ1WinRate = "";
            champ2WinRate = "";
            champ3WinRate = "";
            champ4WinRate = pickChampState != null ? pickChampState.getWinRate().ToString() : "";

            champ0Image = "";
            champ1Image = "";
            champ2Image = "";
            champ3Image = "";
            champ4Image = "";

            string imgpath = String.Format("/Windows/main_champion/{0}_0.jpg", pickChamp.Name);
            champImage = imgpath;

            DateTime start = DateTime.Now;

            while (true)
            {
                JArray? jGameflow = await RiotCLUManager.UsingApiEventJArray(APIMethod.GET, APIEndpoint.GAMEFLOW_PHASE);
                string gameFlow = jGameflow[0].ToString();
                if (gameFlow == "ChampSelect") continue;
                if (gameFlow == "None") break;

                DateTime now = DateTime.Now;
                
                TimeSpan gap = now.Subtract(start);
                champ3Name = gap.ToString(@"mm\:ss");

                await Task.Delay(1000);
            }

            champImage = "";
        }

        public async Task sendGameResult(int pickChampID)
        {
            JArray games = await RiotCLUManager.UsingApiEventJArray(APIMethod.GET, APIEndpoint.CAREER_STATS_SUMMONER(_summoner.puuid));
            JObject? newGame = null;
            foreach (var game in games) 
            {
                if (Convert.ToInt64(game["gamdId"]) == gameId)
                {
                    newGame = (JObject)game; break;
                }
            }
            if (newGame != null)
            {
                JObject data = JObject.Parse(@"{
                    'champion': 10,
                    'result': 1
                }");
                data["champion"] = pickChampID;
                data["result"] = Convert.ToInt32(newGame["stats"]["victory"].ToString());
                await ServerManager.sendGameResult(data);
            }
        }
    }
}
