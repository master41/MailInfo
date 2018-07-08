using TimaivAddIn.Enums;

namespace TimaivAddIn
{
    struct AppInfo
    {
        internal AppInfo(AppType _appType)
        {
            AppType = _appType;
        }

        internal AppType AppType { get; }
    }
}
