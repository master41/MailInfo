using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;

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
                                         object value)
        {

        }

        internal static string GetHeaders(this Outlook.MailItem _mailItem)
        {
            return _mailItem.GetProperty(PR_TRANSPORT_MESSAGE_HEADERS) as string;
        }
        #endregion
    }
}