using System.Collections.Generic;
using System.Linq;
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
            var unreadMails = _service.GetUnreadMails();
            // Log.Debug($"Received {unreadMails.Count()}Mails");
            foreach (var mail in unreadMails)
            {
                var text = mail.Content;
                foreach (var announceService in _announceServices)
                {
                    announceService.Announce(text);
                }
            }

            return Task.CompletedTask;
        }
    }
}