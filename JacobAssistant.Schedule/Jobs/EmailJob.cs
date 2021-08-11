using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JacobAssistant.Services.Email;
using JacobAssistant.Services.Interfaces;
using MimeKit.Text;
using Quartz;
using Serilog;

namespace JacobAssistant.Schedule.Jobs
{
    public class EmailJob : IJob
    {
        private readonly IEnumerable<IAnnounceService> _announceServices;
        private readonly EmailService _service;

        public EmailJob(IEnumerable<IAnnounceService> announceServices, EmailService service)
        {
            _announceServices = announceServices;
            _service = service;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var unreadMails = _service.GetUnreadMails();
                foreach (var mail in unreadMails)
                {
                    foreach (var announceService in _announceServices)
                    {
                        var sb = new StringBuilder();
                        sb.Append($"Subject :{mail.Subject}\n");
                        sb.Append($"From: {mail.Sender}\n");
                        sb.Append($"Date: {mail.Date}\n");
                        sb.Append(mail.Content);
                        announceService.Announce(sb.ToString());
                    }
                }
            }
            catch (Exception)
            {
                Log.Error("Email Job Execute Failed");
                throw;
            }

            return Task.CompletedTask;
        }
    }
}