using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampRecommender.Models;
using ChampRecommender.Windows;

namespace ChampRecommender.ViewModel
{
    internal class RecommendViewModel : BaseViewModel
    {
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

        private ChampRecommend _champRecommend;

        public RecommendViewModel()
        {
            RiotCLUManager.UsingApiEventJObject(ApiMethod.GET, )
        }
    }
}
