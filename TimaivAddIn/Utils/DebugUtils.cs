using System.Diagnostics;

namespace TimaivAddIn.Utils
{
    static class DebugUtils
    {
        internal static void Assert(bool _value)
        {
            Debug.Assert(_value);
        }

        internal static void Stop()
        {
            Assert(false);
        }
    }
}
