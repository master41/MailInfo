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
}
