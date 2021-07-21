using System.Threading.Tasks;
using Quartz;
using Serilog;

namespace JacobAssistant.Schedule.Jobs
{
    public class TestJob:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Log.Information("TestJob");
            return Task.CompletedTask;
        }
    }
}