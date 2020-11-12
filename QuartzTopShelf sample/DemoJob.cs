using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzTopShelf_sample
{
    class DemoJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap= context.JobDetail.JobDataMap;

            string methodName = dataMap.GetString("MethodName");

            Console.WriteLine(DateTime.Now.ToString() +" "+ methodName);

            return Task.FromResult(0);

        }
    }
}
