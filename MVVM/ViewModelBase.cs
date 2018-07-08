using System;
using System.ComponentModel;

namespace MVVM
{
    public class ViewModelBase
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string _name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_name));
        }
        #endregion
    }

    public class StaticViewModelBase
    {
        #region INotifyPropertyChanged Members
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static void OnPropertyChanged(string _name)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(_name));
        }
        #endregion
    }
}
