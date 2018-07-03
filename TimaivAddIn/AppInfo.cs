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
