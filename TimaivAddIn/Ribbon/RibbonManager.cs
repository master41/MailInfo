using System.Windows.Controls;
using TimaivAddIn.CustomTaskPane;
using TimaivAddIn.UserControls;
using TimaivAddIn.Utils;
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
                    DBG_Stop();
                    break;
                case "btnAbout":
                    ShowAboutPane(); break;
                case "btnSettings":
                    ShowSettingsPane(); break;
            }
        }

        internal void ShowAboutPane() => ShowPane<UserControlAbout>();

        internal void ShowSettingsPane() => ShowPane<UserControlSettings>();

        internal CustomTaskPaneManager PaneManager => CustomTaskPaneManager.GetInstance();

        private void ShowPane<T>() where T : UserControl, new()
        {
            PaneManager.InitPane<T>(Globals.ThisAddIn.ActiveExplorer).Show();
        }
    }
}
