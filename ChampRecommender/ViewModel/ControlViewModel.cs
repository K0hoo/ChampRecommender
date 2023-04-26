using ChampRecommender.Models;
using ChampRecommender.Windows;
using System;

namespace ChampRecommender.ViewModel
{
    public class ControlViewModel : SuperViewModel
    {
        public ControlViewModel()
        {
            GameManager.LogicStart(new ShowInformation());
        }

        private string _Point { get; set; }
        public string Point
        {
            get
            {
                return _Point;
            }
            set
            {
                _Point = value;
                OnProperChanged("Point");
            }
        }

        public Array GameTypeArray { get; set; } = Enum.GetValues(typeof(GameManager.GameType));

        private GameManager.GameType _GameTypeValue = GameManager.gameType;
        public GameManager.GameType GameTypeValue
        {
            get
            {
                return _GameTypeValue;
            }
            set
            {
                _GameTypeValue = value;
                GameManager.gameType = value;
            }
        }

        public Action CloseAction { get; set; }
    }
}
