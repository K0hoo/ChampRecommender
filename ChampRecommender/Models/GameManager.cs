﻿using ChampRecommender.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace ChampRecommender.Models
{
    public static class GameManager
    {
        private static int gameWindowX = 0;
        private static int gameWindowY = 0;

        private static ShowInformation mShowInformation;
        public static async void LogicStart(ShowInformation showInfomation)
        {
            showInfomation.Hide();
            if (mShowInformation != null)
            {
                mShowInformation.ThisWindowClose = true;
                await Task.Delay(100);
                mShowInformation.Close();
            }
            mShowInformation = showInfomation;
            
            await 
        }

        private static async Task CheckStartGame()
        {
            gameType = ()
        }
    }
}
