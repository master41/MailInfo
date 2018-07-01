using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;

namespace TimaivAddIn
{
    class ExplorerWrapper
    {
        #region Members
        private Outlook.Explorer explorer;
        #endregion

        #region Constructor
        internal ExplorerWrapper(Outlook.Explorer _explorer)
        {
            if (_explorer == null) return;

            explorer = _explorer;
            AttachEvents();
        }

        private void AttachEvents()
        {
            ((Outlook.ExplorerEvents_10_Event)explorer).SelectionChange += OnSelectionChange;
            ((Outlook.ExplorerEvents_10_Event)explorer).Close += OnClose;
        }

        private void DettachEvents()
        {
            ((Outlook.ExplorerEvents_10_Event)explorer).SelectionChange -= OnSelectionChange;
            ((Outlook.ExplorerEvents_10_Event)explorer).Close -= OnClose;
        }

        private void OnClose()
        {
            DettachEvents();

            Marshal.ReleaseComObject(explorer);
            explorer = null;
        }

        private void OnSelectionChange()
        {
            if (explorer.Selection.Count > 0 && explorer.Selection[1] is Outlook.MailItem mailItem)
            {
                var headers = mailItem.GetHeaders();
            }
        }
        #endregion
    }
}