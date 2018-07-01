using System;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;

namespace TimaivAddIn
{
    public partial class ThisAddIn
    {
        private Outlook.Explorers explorers;

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            explorers = Application.Explorers;

            new ExplorerWrapper(Application.ActiveExplorer());

            ((Outlook.ApplicationEvents_11_Event)Application).Quit += ThisAddIn_Quit;
            ((Outlook.ExplorersEvents_Event)explorers).NewExplorer += ThisAddIn_NewExplorer;
        }

        private void ThisAddIn_NewExplorer(Outlook.Explorer Explorer)
        {

        }

        private void ThisAddIn_Quit()
        {
            ReleaseAddIn();
        }

        private void ReleaseAddIn()
        {
            ((Outlook.ApplicationEvents_11_Event)Application).Quit -= ThisAddIn_Quit;
            ReleaseExplorers();
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
    }
}