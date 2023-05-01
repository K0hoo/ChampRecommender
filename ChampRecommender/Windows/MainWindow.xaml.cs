using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Shell;
using ChampRecommender.ViewModel;

namespace ChampRecommender.Windows
{
    /// <summary>
    /// MainWindon.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientViewModel clientViewModel;

        public MainWindow()
        {
            clientViewModel = new ClientViewModel();
            InitializeComponent();
        }

        public async void ClientOnCheck(object o, EventArgs e)
        {
            while (true)
            {
                if (clientViewModel.CheckClientOn())
                {
                    clientViewModel.Connect();
                    this.MainContent.Content = new ChampRecommend();
                    // this.DataContext = recommendViewModel = new RecommendViewModel();
                    return;
                }
                await Task.Delay(1000 * 10);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object o, RoutedEventArgs e)
        {
            Close();
        }
    }
}
