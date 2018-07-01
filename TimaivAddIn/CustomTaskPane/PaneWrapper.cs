using Tools = Microsoft.Office.Tools;

namespace TimaivAddIn.CustomTaskPane
{
    class PaneWrapper
    {
        internal PaneWrapper(object _window, Tools.CustomTaskPane _pane)
        {
            Window = _window;
            Pane = _pane;
        }

        internal object Window { get; set; }
        internal Tools.CustomTaskPane Pane { get; }
    }
}
