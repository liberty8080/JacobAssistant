using System;
using System.ComponentModel;

namespace JacobAssistant.ScheduleTask
{
    public class JobSchedule
    {
        public Type JobType { get; private set; }
        public string CronExpression { get; private set; }

        public JobStatus JobStatus { get; set; } = JobStatus.Init;

        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }
    }
    
    /// <summary>
    /// Job运行状态
    /// </summary>
    public enum JobStatus:byte
    {
        [Description("初始化")]
        Init=0,
        [Description("运行中")]
        Running=1,
        [Description("调度中")]
        Scheduling = 2,
        [Description("已停止")]
        Stopped = 3,

    }
}