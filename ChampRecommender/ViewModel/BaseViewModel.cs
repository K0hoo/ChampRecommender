
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChampRecommender.ViewModel
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string  propertyName=null)
        {
            PropertyChanged?.Invoke(this, new  PropertyChangedEventArgs(propertyName));
        }
    }
}
