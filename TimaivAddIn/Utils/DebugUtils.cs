using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
