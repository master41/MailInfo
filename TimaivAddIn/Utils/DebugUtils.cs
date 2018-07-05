using System.Diagnostics;

namespace TimaivAddIn.Utils
{
    static class DebugUtils
    {
        internal static void DBG_Assert(bool _value)
        {
            Debug.Assert(_value);
        }

        internal static void DBG_Stop()
        {
            DBG_Assert(false);
        }
    }
}
