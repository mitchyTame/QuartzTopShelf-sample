using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzTopShelf_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            List<JobsAndTriggers> ListJobsAndTriggers = new List<JobsAndTriggers>();

            ListJobsAndTriggers.Add(
                new JobsAndTriggers
                {
                    JobIdentityKey = "Job1",
                    TriggerIdentityKey = "Trigger1",
                    Job_data_MethodName = "Job1_Method()", 
                    ScheduleIntervalInSec =  10
                }
                );
            ListJobsAndTriggers.Add(
                new JobsAndTriggers
                {
                    JobIdentityKey = "Job2",
                    TriggerIdentityKey = "Trigger2",
                    Job_data_MethodName = "Job2_Method()",
                    ScheduleIntervalInSec = 20
                }
                );

            ListJobsAndTriggers.Add(
                new JobsAndTriggers
                {
                    JobIdentityKey = "Job3",
                    TriggerIdentityKey = "Trigger3",
                    Job_data_MethodName = "Job3_Method()",
                    ScheduleIntervalInSec = 40
                }
                ); 

            CreateJobAndTrigger(ListJobsAndTriggers).GetAwaiter().GetResult();

            Console.ReadKey();


        }

        public static async Task CreateJobAndTrigger(List<JobsAndTriggers> listJobsAndTriggers)
        {
            IScheduler  scheduler;
            IJobDetail job = null;
            ITrigger trigger = null;

            var config = (NameValueCollection)ConfigurationManager.GetSection("quartz");
 

            var schedulerFactory = new StdSchedulerFactory(config);

            scheduler = schedulerFactory.GetScheduler().Result;

            scheduler.Start().Wait();

            foreach(var jobAndTrigger in listJobsAndTriggers )
            {
                JobKey jobKey = JobKey.Create(jobAndTrigger.JobIdentityKey);

                job = JobBuilder.Create<DemoJob>()
                    .WithIdentity(jobKey)
                    .UsingJobData("MethodName", jobAndTrigger.Job_data_MethodName)
                    .Build();

                trigger = TriggerBuilder.Create()
                    .WithIdentity(jobAndTrigger.TriggerIdentityKey)
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(jobAndTrigger.ScheduleIntervalInSec).WithRepeatCount(2))
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
