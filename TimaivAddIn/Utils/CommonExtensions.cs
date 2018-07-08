using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimaivAddIn.CustomTaskPane;

namespace TimaivAddIn.Utils
{
    static class CommonExtensions
    {
        internal static void Show(this PaneWrapper _wrapper)
        {
            CustomTaskPaneManager.GetInstance().ShowPane(_wrapper);
        }
    }
}
