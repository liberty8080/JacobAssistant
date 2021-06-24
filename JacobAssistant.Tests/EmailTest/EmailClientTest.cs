using System;
using System.Linq;
using JacobAssistant.Email;
using JacobAssistant.Models;
using JacobAssistant.Services;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Search;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace JacobAssistant.Tests.EmailTest
{
    public class EmailClientTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly EmailAccountService _emailService = new();

        public EmailClientTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void OutlookTest()
        {
            var outlook = _emailService.GetAOutlook();
            using var client = new OutlookClient {Username = outlook.Email, Passwd = outlook.Password};
            try
            {
                var unreadMails = client.UnreadMails();
                _testOutputHelper.WriteLine(unreadMails.ToString());

            }
            catch (AuthenticationException e)
            {
                _testOutputHelper.WriteLine("登陆失败!");
            }
            
        }

        [Fact]
        public void GmailTest()
        {
            var gmail = _emailService.GetAGmail();
            using var client = new GmailClient{Username = gmail.Email, Passwd = gmail.Password};
            try
            {
                var unread = client.UnreadMails();
                _testOutputHelper.WriteLine(unread.ToString());
            }
            catch (AuthenticationException e)
            {
                _testOutputHelper.WriteLine($"登录失败{e}");
            }   
        }
    }
}