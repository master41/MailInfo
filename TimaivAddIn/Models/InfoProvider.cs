using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TimaivAddIn.Models
{
    class InfoProvider
    {
        internal Outlook.MailItem Source { get; private set; }

        internal InfoProvider(Outlook.MailItem mailItem)
        {
            Source = mailItem;
        }

        internal MailItemInfo GetInfo()
        {
            return new MailItemInfo {
                // ...

                HeadersInfo = ParseHeaders()
            };
        }

        internal MailItemHeadersInfo ParseHeaders()
        {
            // ...

            return new MailItemHeadersInfo();
        }
    }
}
