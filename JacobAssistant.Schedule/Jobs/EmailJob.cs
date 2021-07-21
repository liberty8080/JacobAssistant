using System.Collections.Generic;
using System.Threading.Tasks;
using JacobAssistant.Services.Email;
using JacobAssistant.Services.Interfaces;
using MimeKit.Text;
using Quartz;

namespace JacobAssistant.Schedule.Jobs
{
    public class EmailJob : IJob
    {
        private readonly IEnumerable<IAnnounceService> _announceServices;
        private readonly EmailHandler _handler;

        public EmailJob(IEnumerable<IAnnounceService> announceServices, EmailHandler handler)
        {
            _announceServices = announceServices;
            _handler = handler;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var unreadMails = _handler.GetUnreadMails();
            foreach (var mail in unreadMails)
            {
                var text = mail.GetTextBody(TextFormat.Text);
                foreach (var announceService in _announceServices)
                {
                    announceService.Announce(text);
                }
            }

            return Task.CompletedTask;
        }
    }
}