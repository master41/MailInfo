using Tools = Microsoft.Office.Tools;

namespace TimaivAddIn.CustomTaskPane
{
    class PaneWrapper
    {
        internal object Window { get; set; }
        internal Tools.CustomTaskPane Pane { get; }
    }
}
