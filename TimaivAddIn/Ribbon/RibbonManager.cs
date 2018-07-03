using System.Windows.Controls;
using TimaivAddIn.CustomTaskPane;
using TimaivAddIn.UserControls;
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
            ShowPane<UserControlAbout>();
        }

        private void ShowPane<T>() where T : UserControl
        {
            CustomTaskPaneManager.GetInstance().InitPane<T>(Globals.ThisAddIn.ActiveExplorer);
        }
    }
}
