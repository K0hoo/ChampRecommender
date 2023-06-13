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
            JObject currentChamp = JObject.Parse(@"{
                'line': 1, // top: 1, jungle: 2, mid: 3, bottom: 4, utility: 5
                'myTeam': {
                    'top': 0,
                    'jungle': 254,
                    'middle': 7,
                    'bottom': 81,
                    'utility': 497
                },
                'theirTeam': [79, 518, 91, 29, 497],
                'ban': [0, 0, 516, 0, 240, 0, 0, 0, 0, 0]
            }");
            JArray result = await ServerManager.getRecommendation(currentChamp);
            Trace.WriteLine(result.ToString());
        }
    }
}