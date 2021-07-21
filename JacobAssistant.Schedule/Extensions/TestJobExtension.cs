using JacobAssistant.Schedule.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace JacobAssistant.Schedule.Extensions
{
    public static class TestJobExtension
    {
        public static IServiceCollection AddTestJob(this IServiceCollection services)
        {
            services.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionScopedJobFactory();
                    var jobKey = new JobKey("TestJobKey");
                    q.AddJob<TestJob>(opt => opt.WithIdentity(jobKey));
                    q.AddTrigger(opt => opt
                        .ForJob(jobKey)
                        .WithIdentity("TestJob-trigger")
                        .WithCronSchedule("0/5 * * * * ?"));
                }
            );

            return services;
        }
    }
}