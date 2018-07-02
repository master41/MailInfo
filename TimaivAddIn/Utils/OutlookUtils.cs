using System;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;
using TimaivAddIn.Enums;

namespace TimaivAddIn
{
    static class OutlookUtils
    {
        #region Constants
        private const string PR_TRANSPORT_MESSAGE_HEADERS = "http://schemas.microsoft.com/mapi/proptag/0x007D001E";
        #endregion

        #region Methods
        internal static object GetProperty(this Outlook.MailItem _mailItem, string _propertyName)
        {
            if (_mailItem == null) throw new ArgumentNullException();

            Outlook.PropertyAccessor propertyAccessor = null;
            try
            {
                propertyAccessor = _mailItem.PropertyAccessor;

                if (propertyAccessor != null)
                {
                    return propertyAccessor.GetProperty(_propertyName);
                }
            }
            catch (COMException)
            {

            }
            finally
            {
                if (propertyAccessor != null)
                {
                    Marshal.ReleaseComObject(propertyAccessor);
                    propertyAccessor = null;
                }
            }

            return null;
        }

        internal static void SetProperty(this Outlook.MailItem _mailItem,
                                         string _propertyName,
                                         object _value)
        {
            if (_mailItem == null) throw new ArgumentNullException();

            Outlook.PropertyAccessor propertyAccessor = null;
            try
            {
                propertyAccessor = _mailItem.PropertyAccessor;

                if (propertyAccessor != null)
                {
                    propertyAccessor.SetProperty(_propertyName, _value);
                }
            }
            catch (COMException)
            {

            }
            finally
            {
                if (propertyAccessor != null)
                {
                    Marshal.ReleaseComObject(propertyAccessor);
                    propertyAccessor = null;
                }
            }
        }

        internal static string GetHeaders(this Outlook.MailItem _mailItem)
        {
            return _mailItem.GetProperty(PR_TRANSPORT_MESSAGE_HEADERS) as string;
        }

        internal static AppInfo GetAppInfo()
        {
            AppType appType = default(AppType);

            return new AppInfo(appType);
        }
        #endregion
    }
}