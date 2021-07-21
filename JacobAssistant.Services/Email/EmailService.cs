using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Models;
using MimeKit;
using MimeKit.Text;

namespace JacobAssistant.Services.Email
{
    public class EmailService
    {
        private readonly ConfigurationDbContext _context;

        public EmailService(ConfigurationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get unread mail for all account
        /// </summary>
        /// <returns>unread mails</returns>
        public IEnumerable<EmailMessage> GetUnreadMails()
        {
            var accounts = EmailAccounts();
            var totalUnread = new List<EmailMessage>();
            foreach (var emailAccount in accounts)
            {
                using var client = EmailClient(emailAccount);
                var unread = client.UnreadMails();
                foreach (var mail in unread)
                {
                    var query = _context.EmailMessages.Any(message => message.Id == mail.MessageId);
                    if (!query)
                    {
                        var emailMessage = new EmailMessage()
                        {
                            Id = mail.MessageId,
                            Subject = mail.Subject,
                            Date = mail.Date,
                            Content = mail.GetTextBody(TextFormat.Plain),
                            Sender = mail.Sender==null?mail.From.ToString():mail.Sender.ToString(),
                            Priority = mail.Priority.ToString(),
                            To = mail.To.ToString()
                        };
                        totalUnread.Add(emailMessage);
                        _context.EmailMessages.Add(emailMessage);
                        _context.SaveChanges();
                    }
                }
                // totalUnread.AddRange(unread);
            }

            return totalUnread;
        }

        private IEnumerable<EmailAccount> EmailAccounts()
        {
            return _context.EmailAccounts.Where(account => account.State==(int)EmailAccountState.Active).ToList();
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