using MVVM;

namespace TimaivAddIn.ViewModels.ViewModelAbout
{
    class ViewModelAbout : ViewModelBase
    {
        private string version;
        public string Version
        {
            get => version;
            set
            {
                if (version != value)
                {
                    version = value;
                    OnPropertyChanged("Version");
                }
            }
        }
    }
}