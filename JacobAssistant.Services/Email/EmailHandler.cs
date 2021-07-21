using System;
using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Models;
using JacobAssistant.Services.Interfaces;
using MimeKit;

namespace JacobAssistant.Services.Email
{
    public class EmailHandler
    {
        // private readonly IAnnounceService _bot;
        private readonly IAnnounceService _announceService;
        private readonly EmailAccountService _service;
        // private List<string> _messages = new();

        public EmailHandler(IAnnounceService announceService, EmailAccountService service)
        {
            // _bot = bot;
            _announceService = announceService;
            _service = service;
        }

        public IEnumerable<MimeMessage> GetUnreadMails()
        {
            var accounts = _service.EmailAccounts();
            Console.WriteLine(accounts.Count);
            var totalUnread = new List<MimeMessage>();
            foreach (var emailAccount in accounts)
            {
                using var client = EmailClient(emailAccount);
                var unread = client.UnreadMails();
                totalUnread.AddRange(unread);
            }

            return totalUnread;
        }

        private static CustomEmailImapClient EmailClient(EmailAccount account)
        {
            return account.Type switch
            {
                1 => new OutlookClient {Username = account.Email, Passwd = account.Password},
                2 => new GmailClient {Username = account.Email, Passwd = account.Password},
                _ => null
            };
        }
    }
}