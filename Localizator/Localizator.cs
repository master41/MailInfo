using MVVM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localizator
{
    public class Lozalizator : StaticViewModelBase
    {
        #region Lazy Singleton
        private static Lozalizator instance;
        internal static Lozalizator GetInstance() => instance ?? (instance = new Lozalizator());
        #endregion

        #region Ctor
        private Lozalizator() { Init(); }
        #endregion

        #region Property
        private CultureInfo currentCulture;
        public CultureInfo CurrentCulture
        {
            get => currentCulture;
            set
            {
                if (currentCulture != value)
                {
                    currentCulture = value;
                    OnPropertyChanged("CurrentCulture");
                }
            }
        }

        private List<CultureInfo> languages = new List<CultureInfo>();
        private Dictionary<string, string> CurrentDictionary;
        #endregion

        #region Methods
        private void Init()
        {
            InitAvailableLanguages();
            InitCurrentLanguage();
        }

        private void InitCurrentLanguage()
        {

        }

        private void InitAvailableLanguages()
        {

        }

        private string Localize(string _uid)
        {
            if (CurrentDictionary.TryGetValue(_uid, out string _value))
            {
                return _value;
            }
            return null;
        }

        private void UpdateConfig()
        {

        }
        #endregion

        #region Events
        public event Action OnLanguageChanged;
        public event Action OnLanguageInitFail;
        public event Action OnLanguagesInit;
        #endregion
    }
}
