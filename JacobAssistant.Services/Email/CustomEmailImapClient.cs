using System.Collections.Generic;
using System.Linq;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace JacobAssistant.Services.Email
{
    public class CustomEmailImapClient : ImapClient
    {
        protected string Host { get; set; }
        protected int Port { get; set; }
        protected bool UseSsl { get; set; }

        public string Username { get; set; }
        public string Passwd { get; set; }

        public void Connect()
        {
            base.Connect(Host, Port, UseSsl);
        }

        public void Auth()
        {
            Authenticate(Username, Passwd);
        }

        public List<MimeMessage> UnreadMails()
        {
            Connect();
            if (!IsAuthenticated)
            {
                Authenticate(Username, Passwd);
            }

            Inbox.Open(FolderAccess.ReadOnly);
            var unreadNum = Inbox.Unread;
            var unreadUniqueIds = Inbox.Search(SearchQuery.NotSeen);
            var unread = unreadUniqueIds.Select(id => Inbox.GetMessage(id)).ToList();
            Disconnect(true);
            return unread;
        }
    }
}