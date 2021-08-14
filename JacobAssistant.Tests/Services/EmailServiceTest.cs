using JacobAssistant.Common.Models;
using JacobAssistant.Services.Email;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Serilog;

namespace JacobAssistant.Tests.Services
{
    [TestFixture]
    public class EmailServiceTest:BaseTest
    {
        [Test]
        public void UnreadMailsTest()
        {
            var client = new GmailClient {Username = "zhaojiawei233@gmail.com", Passwd = "xntiqkngixtqpxlw"};
            foreach (var mail in client.UnreadMails())
            {
                Log.Information(mail.Subject);
            }
        }
    }
}