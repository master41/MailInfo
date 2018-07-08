using MVVM;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace TimaivAddIn.ViewModels.ViewModelSettings
{
    class ViewModelSettings : ViewModelBase
    {
        #region Constructor
        internal ViewModelSettings()
        {
            InitSettings();
        }
        #endregion

        #region Property
        private ObservableCollection<CultureInfo> languages;
        public ObservableCollection<CultureInfo> Languages
        {
            get => languages;
            set
            {
                if (languages != value)
                {
                    languages = value;
                    OnPropertyChanged("Languages");
                }
            }
        }

        private CultureInfo selectedLanguage;
        public CultureInfo SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;
                    if (IsDataLoaded) UpdateLanguage();
                    OnPropertyChanged("SelectedLanguage");
                }
            }
        }
        #endregion

        #region Methods
        private void UpdateLanguage()
        {

        }

        private void InitSettings()
        {
            Languages = new ObservableCollection<CultureInfo>();
        }

        private void OnRequestSetDefaults()
        {

        }
        #endregion

        #region Property
        internal bool IsDataLoaded { get; set; }
        #endregion

        #region Commands
        private ICommand setDefaults;
        public ICommand SetDefaults => setDefaults ?? (setDefaults = new DelegateCommand(OnRequestSetDefaults));
        #endregion
    }
}
