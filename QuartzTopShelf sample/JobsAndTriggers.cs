using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzTopShelf_sample
{
    class JobsAndTriggers
    {
        public string JobIdentityKey { get; set;}
        public string TriggerIdentityKey { get; set;}
        public string Job_data_MethodName { get; set;}

        public int ScheduleIntervalInSec { get; set; }
    }
}
