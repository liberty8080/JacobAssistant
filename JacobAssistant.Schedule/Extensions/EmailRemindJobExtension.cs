using JacobAssistant.Common.Models;
using JacobAssistant.Schedule.Jobs;
using JacobAssistant.Services;
using JacobAssistant.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace JacobAssistant.Schedule.Extensions
{
    public static class EmailRemindJobExtension
    {
        public static IServiceCollection AddEmailRemindJob(this IServiceCollection services)
        {
            services.AddSingleton<EmailHandler,EmailHandler>();
            services.AddSingleton<EmailAccountService, EmailAccountService>(provider => 
                new EmailAccountService(provider.CreateScope().ServiceProvider.GetService<ConfigurationDbContext>()));
            services.AddQuartz(q =>
                            {
                                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                                var jobKey = new JobKey("EmailJobKey");
                                q.AddJob<EmailJob>(opt => opt.WithIdentity(jobKey));
                                q.AddTrigger(opt => opt
                                    .ForJob(jobKey)
                                    .WithIdentity("EmailJob-trigger")
                                    .WithCronSchedule("0/30 * * * * ?"));
                            }
                        );
            return services;
        }
    }
}