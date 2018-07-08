using System;
using System.ComponentModel;

namespace MVVM
{
    public class ViewModelBase
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string txt)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(txt));
        }
        #endregion
    }

    public class StaticViewModelBase
    {
        #region INotifyPropertyChanged Members
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static void RaiseStaticPropertyChanged(string propName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
