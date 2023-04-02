using ChampRecommender.Models;
// using ChampRecommender.ViewModel;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace ChampRecommender.Windows
{
    /// <summary>
    /// WaitForOnGame.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WaitForOnGame : Window
    {
        public WaitForOnGame()
        {
            InitializeComponent();
        }

        public async void WaitForGame(object o, EventArgs e)
        {
            Directory.SetCurrentDirectory(Process.GetCurrentProcess().MainModule.FileName.Split(new string[] { "\\ChampRecommender.exe" }, StringSplitOptions.None)[0]);
            Hide();
            while (true)
            {
                if (GetProcessStatus.FindGameOnProcess())
                {
                
                }
                await Task.Delay(1000 * 10);
            }

        }
    }
}
