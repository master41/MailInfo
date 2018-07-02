using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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