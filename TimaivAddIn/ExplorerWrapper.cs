using Outlook = Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;

namespace TimaivAddIn
{
    class ExplorerWrapper
    {
        #region Members
        private Outlook.Explorer explorer;
        private MailWrapper mailWrapper;
        private string mailEntryId;
        #endregion

        #region Constructor
        internal ExplorerWrapper(Outlook.Explorer _explorer)
        {
            if (_explorer == null) return;

            explorer = _explorer;
            AttachEvents();
        }
        #endregion

        #region Methods
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
            mailWrapper?.Dispose();

            DettachEvents();

            Marshal.ReleaseComObject(explorer);
            explorer = null;
        }

        private void OnSelectionChange()
        {
            if (explorer.Selection.Count > 0 && explorer.Selection[1] is Outlook.MailItem mailItem)
            {
                if (mailEntryId != mailItem.EntryID)
                {
                    mailWrapper?.Dispose();

                    mailEntryId = mailItem.EntryID;
                    mailWrapper = new MailWrapper(mailItem, explorer);
                    var headers = mailItem.GetHeaders();
                }
            }
        }
        #endregion
    }
}