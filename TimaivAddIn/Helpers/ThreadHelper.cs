using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimaivAddIn.Helpers
{
    static class ThreadHelper
    {
        internal static void InvokeInUI(this Action _action)
        {
            Globals.ThisAddIn.UIDispatcher.Invoke(_action);
        }
    }
}
