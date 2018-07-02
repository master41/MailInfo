using MVVM;
using Tools = Microsoft.Office.Tools;

namespace TimaivAddIn.CustomTaskPane
{
    class PaneWrapper
    {
        internal PaneWrapper(object _window, ViewModelBase _vm, Tools.CustomTaskPane _pane)
        {
            Window = _window;
            ViewModel = _vm;
            Pane = _pane;
        }

        internal object Window { get; set; }
        internal ViewModelBase ViewModel { get; set; }
        internal Tools.CustomTaskPane Pane { get; }
    }
}
