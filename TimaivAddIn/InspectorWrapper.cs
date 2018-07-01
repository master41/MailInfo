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
    class InspectorWrapper
    {
        #region Members
        private Outlook.Inspector inspector;
        private Outlook.MailItem mailItem;
        #endregion

        #region Constructor
        internal InspectorWrapper(Outlook.Inspector _inspector)
        {
            if (_inspector == null) return;

            inspector = _inspector;
            AttachEvents();
        }

        private void AttachEvents()
        {
            ((Outlook.InspectorEvents_10_Event)inspector).Close += OnClose;
        }

        private void DettachEvents()
        {
            ((Outlook.InspectorEvents_10_Event)inspector).Close -= OnClose;
        }

        private void OnClose()
        {
            DettachEvents();

            if (inspector != null)
            {
                Marshal.ReleaseComObject(inspector);
                inspector = null;
            }
            if (mailItem != null)
            {
                Marshal.ReleaseComObject(mailItem);
                mailItem = null;
            }
        }
        #endregion
    }
}