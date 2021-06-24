using System;
using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Bot;
using JacobAssistant.Models;
using JacobAssistant.Services;
using MimeKit;

namespace JacobAssistant.Email
{
    public class EmailHandler
    {
        private readonly AssistantBotClient _bot;
        private readonly EmailAccountService _service;
        private List<string> _messages = new();
        public EmailHandler(AssistantBotClient bot,EmailAccountService service)
        {
            _bot = bot;
            _service = service;
        }

        public void GetUnreadMails()
        {
            var accounts = _service.EmailAccounts();
            Console.WriteLine(accounts.Count);
            foreach (var emailAccount in accounts)
            {
                using var client = EmailClient(emailAccount);
                var unread =client.UnreadMails();
                foreach (var u in unread.Where(u => !_messages.Contains(u.MessageId)))
                {
                    var announce = $"标题:{u.Subject}\n发件人:{u.From}\n";
                    _bot.SendMessageToChannel(announce);
                    _messages.Add(u.MessageId);
                }
                
            }
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