using Outlook = Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;
using System;

namespace TimaivAddIn
{
    class MailWrapper : IDisposable
    {
        #region Members
        private object window;
        private Outlook.MailItem mailItem;
        #endregion

        #region Constructor
        internal MailWrapper(Outlook.MailItem _mailItem, object _window)
        {
            if (_mailItem == null || window == null) return;

            window = _window;
            AttachEvents();
        }

        private void AttachEvents()
        {
            ((Outlook.ItemEvents_10_Event)mailItem).Close += OnClose;
        }

        private void DettachEvents()
        {
            ((Outlook.ItemEvents_10_Event)mailItem).Close -= OnClose;
        }

        private void OnClose(ref bool Cancel)
        {
            Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DettachEvents();

                    if (window is Outlook.Inspector)
                    {
                        Marshal.ReleaseComObject(window);
                        window = null;
                    }

                    if (mailItem != null)
                    {
                        Marshal.ReleaseComObject(mailItem);
                        mailItem = null;
                    }
                }

                disposedValue = true;
            }
        }

        ~MailWrapper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }
}