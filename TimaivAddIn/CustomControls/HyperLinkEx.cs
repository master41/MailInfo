using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace TimaivAddIn.CustomControls
{
    class HyperLinkEx : Hyperlink
    {
        #region Constructor
        internal HyperLinkEx()
        {
            Unloaded += HyperLinkEx_Unloaded;
            RequestNavigate += OnRequestNavigate;
        }
        #endregion

        #region Methods
        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void HyperLinkEx_Unloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= HyperLinkEx_Unloaded;
            RequestNavigate -= OnRequestNavigate;
        }
        #endregion
    }
}
