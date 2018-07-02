using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimaivAddIn.CustomTaskPane;
using static TimaivAddIn.Utils.DebugUtils;

namespace TimaivAddIn.Ribbon
{
    class eRibbonManager
    {
        internal eRibbonManager() { }

        internal void OnClick(string _id)
        {
            switch (_id)
            {
                default:
                    Stop();
                    break;
                case "btnAbout":
                    ShowAboutPane(); break;
            }
        }

        internal void ShowAboutPane()
        {
            CustomTaskPaneManager.GetInstance().InitPane<U>(Globals.ThisAddIn.ActiveExplorer, null);
        }
    }
}
