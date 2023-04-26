using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampRecommender.ViewModel
{
    public class showInformationViewModel : SuperViewModel
    {
        public Action? CloseAction { get; set; }

        public static showInformationViewModel? viewModelObject;

        public showInformationViewModel()
        {
            viewModelObject = this;
            if (Lo)
        }
    }
}
