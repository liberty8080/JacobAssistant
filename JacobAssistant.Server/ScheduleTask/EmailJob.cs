using System;
using System.Threading.Tasks;
using JacobAssistant.Email;
using JacobAssistant.Services;
using Quartz;
using Serilog;

namespace JacobAssistant.ScheduleTask
{
    public class EmailJob : IJob
    {
        private readonly EmailHandler _handler;

        public EmailJob(EmailHandler handler)
        {
            _handler = handler;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _handler.GetUnreadMails();
            return Task.CompletedTask;
        }
    }
}