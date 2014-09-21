using Controls.Threading.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading.Work
{
    internal class ActionWork : Work
    {
        private readonly Action action;

        internal ActionWork(ITaskScheduler taskScheduler, Action action)
            : base(taskScheduler)
        {
            this.action = action;
        }

        protected override void Execute()
        {
            this.action.Invoke();
        }
    }
}