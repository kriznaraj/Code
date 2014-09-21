using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading.Scheduler
{
    internal interface ITaskScheduler
    {
        void NotifyComplete();

        void Schedule(Task task);
    }
}