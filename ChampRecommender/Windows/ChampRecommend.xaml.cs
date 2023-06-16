using ChampRecommender.Dataset;
using ChampRecommender.Models;
using ChampRecommender.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampRecommender.Windows
{
    /// <summary>
    /// WaitForGame.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChampRecommend : Page
    {

        RecommendViewModel recommendViewModel;

        public List<ChampionStatics>? championStatics;
        public ChampRecommend()
        {
            InitializeComponent();

            this.DataContext = recommendViewModel = new RecommendViewModel();

            initChampRecommend();
        }

        private async void initChampRecommend()
        {
            await recommendViewModel.initRecommendViewModel();
            /*
            await recommendViewModel.setSummoner();
            await recommendViewModel.SetMostChampion();
            */
        }

        private async void communicateTest(object sender, RoutedEventArgs e)
        { 
            /*JObject currentChamp = JObject.Parse(@"{
                'line': 4, // top: 1, jungle: 2, mid: 3, bottom: 4, utility: 5
                'myTeam': {
                    'top': 54,
                    'jungle': 57,
                    'middle': 127,
                    'bottom': 0,
                    'utility': 117
                },
                'theirTeam': [897, 64, 7, 429, 89],
                'ban': [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                'most_champion': [],
                'possible_champion': []
            }");

            championStatics = gameStatics.GetChampionStatics();

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

            JArray? pickableChampion = await RiotCLUManager.UsingApiEventJArray(APIMethod.GET, APIEndpoint.PICKABLE_CHAMP_IDS);
            if (pickableChampion != null) currentChamp["possible_champion"] = pickableChampion;

            JArray result = await ServerManager.getRecommendation(currentChamp);
            Trace.WriteLine(result.ToString());*/


            await recommendViewModel.sendGameResult(10);
        }
    }
}