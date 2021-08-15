using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using JacobAssistant.Common.Models;
using MimeKit;
using MimeKit.Text;
using Serilog;

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

                foreach (var mimeMessage in unread)
                {
                    try
                    {
                        var query = _context.EmailMessages.Any(m => m.Id == mimeMessage.MessageId);
                        if (!query)
                        {
                            var emailMessage = new EmailMessage
                            {
                                Id = mimeMessage.MessageId,
                                Subject = mimeMessage.Subject,
                                Date = mimeMessage.Date,
                                Content = mimeMessage.GetTextBody(TextFormat.Plain),
                                Sender = mimeMessage.Sender == null
                                    ? mimeMessage.From.ToString()
                                    : mimeMessage.Sender.ToString(),
                                Priority = mimeMessage.Priority.ToString(),
                                To = mimeMessage.To.ToString()
                            };
                            totalUnread.Add(emailMessage);
                            _context.EmailMessages.Add(emailMessage);
                            _context.SaveChanges();
                        }
                    }
                    catch (SocketException)
                    {
                        Log.Error($"Network Error! {emailAccount.Email} Can't Update");
                    }
                    catch (MailKit.Security.SslHandshakeException)
                    {
                        Log.Error($"Network Error! {emailAccount.Email} Can't Update");
                    }
                    catch (Exception e)
                    {
                        throw new EmailException("Unread Mails Fetch Failed ", e);
                    }
                }
            }

            return totalUnread;
        }

        private IEnumerable<EmailAccount> EmailAccounts()
        {
            return _context.EmailAccounts.Where(account => account.State == (int) EmailAccountState.Active).ToList();
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