using System;
using System.Threading;

namespace TimaivAddIn.Helpers
{
    static class ThreadHelper
    {
        internal static void InvokeInUI(this Action _action)
        {
            Globals.ThisAddIn.UIDispatcher.Invoke(_action);
        }

        internal static void InvokeInBackgroundThread(this Action _action)
        {
            Thread thread = new Thread(new ThreadStart(_action));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
