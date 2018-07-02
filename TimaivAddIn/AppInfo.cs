using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimaivAddIn.Enums;

namespace TimaivAddIn
{
    class AppInfo
    {
        internal AppInfo(AppType _appType)
        {
            AppType = _appType;
        }

        internal AppType AppType { get; }
    }
}
