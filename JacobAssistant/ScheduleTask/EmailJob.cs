using System;
using System.Threading.Tasks;
using JacobAssistant.Bot;
using JacobAssistant.Email;
using JacobAssistant.Services;
using Quartz;

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
            Console.WriteLine("执行");
            return Task.CompletedTask;
        }
    }
}