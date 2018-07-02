using System;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TimaivAddIn
{
    public partial class ThisAddIn
    {
        #region Private Members
        private Outlook.Explorers explorers;
        private Outlook.Inspectors inspectors;
        #endregion

        #region Property
        internal Dispatcher UIDispatcher { get; set; }
        internal static AppInfo AppInfo { get; } = OutlookUtils.GetAppInfo();
        #endregion

        #region Private Methods
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            UIDispatcher = Dispatcher.CurrentDispatcher;

            explorers = Application.Explorers;
            inspectors = Application.Inspectors;

            new ExplorerWrapper(Application.ActiveExplorer());

            ((Outlook.ApplicationEvents_11_Event)Application).Quit += ThisAddIn_Quit;
            ((Outlook.ExplorersEvents_Event)explorers).NewExplorer += ThisAddIn_NewExplorer;
            ((Outlook.InspectorsEvents_Event)inspectors).NewInspector += ThisAddIn_NewInspector;
        }

        private void ThisAddIn_NewInspector(Outlook.Inspector _inspector)
        {
            if (_inspector.CurrentItem is Outlook.MailItem mailItem)
            {
                new MailWrapper(mailItem, _inspector);
            }
        }

        private void ThisAddIn_NewExplorer(Outlook.Explorer _explorer)
        {
            new ExplorerWrapper(_explorer);
        }

        private void ThisAddIn_Quit()
        {
            ReleaseAddIn();
        }

        private void ReleaseAddIn()
        {
            ((Outlook.ApplicationEvents_11_Event)Application).Quit -= ThisAddIn_Quit;
            ReleaseExplorers();
            ReleaseInspectors();
        }

        private void ReleaseInspectors()
        {
            if (inspectors != null)
            {
                ((Outlook.InspectorsEvents_Event)inspectors).NewInspector -= ThisAddIn_NewInspector;

                Marshal.ReleaseComObject(inspectors);
                inspectors = null;
            }
        }

        private void ReleaseExplorers()
        {
            if (explorers != null)
            {
                ((Outlook.ExplorersEvents_Event)explorers).NewExplorer -= ThisAddIn_NewExplorer;

                Marshal.ReleaseComObject(explorers);
                explorers = null;
            }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            ReleaseAddIn();
        }

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon1();
        }

        #region VSTO generated code
        private void InternalStartup()
        {
            this.Startup += new EventHandler(ThisAddIn_Startup);
            this.Shutdown += new EventHandler(ThisAddIn_Shutdown);
        }
        #endregion

        #endregion
    }
}