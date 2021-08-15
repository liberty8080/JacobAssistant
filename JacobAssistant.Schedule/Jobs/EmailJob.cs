using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JacobAssistant.Services.Email;
using JacobAssistant.Services.Interfaces;
using Quartz;
using Serilog;

namespace JacobAssistant.Schedule.Jobs
{
    [DisallowConcurrentExecution]
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
            catch (IOException)
            {
                Log.Error("NetworkError in fetching Email");
            }
            catch (Exception e)
            {
                Log.Error(e,"Email Job Execute Failed");
            }

            return Task.CompletedTask;
        }
    }
}