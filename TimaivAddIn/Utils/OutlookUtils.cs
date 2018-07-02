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
        private const string PR_SMTP_ADDRESS = @"http://schemas.microsoft.com/mapi/proptag/0x39FE001E";
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

        internal static string GetSenderSMTPAddress(this Outlook.MailItem _mail)
        {
            if (_mail == null) throw new ArgumentNullException();

            if (_mail.SenderEmailType == "EX")
            {
                Outlook.AddressEntry sender = null;
                try
                {
                    sender = _mail.Sender;
                    if (sender != null)
                    {
                        //Now we have an AddressEntry representing the Sender
                        if (sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry
                         || sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeRemoteUserAddressEntry)
                        {
                            //Use the ExchangeUser object PrimarySMTPAddress
                            Outlook.ExchangeUser exchUser = null;
                            try
                            {
                                exchUser = sender.GetExchangeUser();
                                if (exchUser != null)
                                {
                                    return exchUser.PrimarySmtpAddress;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            finally
                            {
                                if (exchUser != null)
                                {
                                    Marshal.ReleaseComObject(exchUser);
                                    exchUser = null;
                                }
                            }
                        }
                        else
                        {
                            Outlook.PropertyAccessor propertyAccessor = null;
                            try
                            {
                                propertyAccessor = sender.PropertyAccessor;
                                if (propertyAccessor != null)
                                {
                                    try
                                    {
                                        return propertyAccessor.GetProperty(PR_SMTP_ADDRESS) as string;
                                    }
                                    catch (COMException) { }
                                }
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
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    if (sender != null)
                    {
                        Marshal.ReleaseComObject(sender);
                        sender = null;
                    }
                }
            }
            else
            {
                return _mail.SenderEmailAddress;
            }

            return _mail.SenderEmailAddress;
        }

        internal static Outlook.Account GetAccount(this Outlook.MailItem _mail)
        {
            if (_mail == null) throw new ArgumentNullException();

            Outlook.Folder folder = null;
            try
            {
                folder = _mail.Parent as Outlook.Folder;
                return GetAccount(folder);
            }
            finally
            {
                if (folder != null)
                {
                    Marshal.ReleaseComObject(folder);
                    folder = null;
                }
            }
        }

        internal static Outlook.Account GetAccount(this Outlook.Folder _folder)
        {
            Outlook.Store store = null;
            Outlook.NameSpace session = null;
            Outlook.Accounts accounts = null;

            try
            {
                store = _folder.Store;
                session = _folder.Session;
                accounts = session.Accounts;

                foreach (Outlook.Account account in accounts)
                {
                    Outlook.Store accountStore = null;

                    try
                    {
                        accountStore = account.DeliveryStore;
                        if (accountStore.StoreID == store.StoreID)
                        {
                            return account;
                        }
                        else
                        {
                            Marshal.ReleaseComObject(account);
                        }
                    }
                    finally
                    {
                        if (accountStore != null)
                        {
                            Marshal.ReleaseComObject(accountStore);
                            accountStore = null;
                        }
                    }
                }
            }
            finally
            {
                if (store != null)
                {
                    Marshal.ReleaseComObject(store);
                    store = null;
                }

                if (session != null)
                {
                    Marshal.ReleaseComObject(session);
                    session = null;
                }

                if (accounts != null)
                {
                    Marshal.ReleaseComObject(accounts);
                    accounts = null;
                }
            }

            return null;
        }

        internal static string GetHeaders(this Outlook.MailItem _mailItem)
        {
            return _mailItem.GetProperty(PR_TRANSPORT_MESSAGE_HEADERS) as string;
        }

        internal static AppInfo GetAppInfo()
        {
            AppType appType = AppType.UNDEFINED;

            string version = Globals.ThisAddIn.Application.Version;
                        
            return new AppInfo(appType);
        }
        #endregion
    }
}