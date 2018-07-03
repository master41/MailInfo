using MVVM;
using TimaivAddIn.Attributes;
using Tools = Microsoft.Office.Tools;

namespace TimaivAddIn.CustomTaskPane
{
    class PaneWrapper
    {
        internal PaneWrapper(object _window, ViewModelBase _vm, Tools.CustomTaskPane _pane, int _id = 0)
        {
            Window = _window;
            ViewModel = _vm;
            Pane = _pane;
            Id = _id;
        }

        [Key]
        internal int Id { get; }

        internal object Window { get; set; }
        internal ViewModelBase ViewModel { get; set; }
        internal Tools.CustomTaskPane Pane { get; }
    }
}
