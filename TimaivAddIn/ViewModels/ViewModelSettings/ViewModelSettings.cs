using MVVM;
using System;
using System.Globalization;

namespace TimaivAddIn.ViewModels.ViewModelSettings
{
    class ViewModelSettings : ViewModelBase
    {
        #region Property
        private CultureInfo languages;
        public CultureInfo Languages
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
        #endregion

        #region Property
        internal bool IsDataLoaded { get; set; }
        #endregion
    }
}
