﻿using System;
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

namespace ChampRecommender.Windows
{
    /// <summary>
    /// MainWindon.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public async void ClassicGameOn(object o, EventArgs e)
        {
            await Task.Delay(1000 * 10);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object o, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ChangeContent(object sender, RoutedEventArgs e)
        {
            if (this.MainContent is System.Windows.Controls.Frame)
            {
                this.MainContent.Content = new WaitForGame();
            }
            else
            {
                this.MainContent.Content = new System.Windows.Controls.Frame();
            }
        }
    }
}