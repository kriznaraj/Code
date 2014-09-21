using Controls.Threading.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Threading.Provider
{
    internal class UserThreadProvider : IThreadProvider
    {
        private static readonly UserThreadProvider instance = new UserThreadProvider();

        private UserThreadProvider()
        {
        }

        internal static UserThreadProvider Instance
        {
            get { return UserThreadProvider.instance; }
        }

        public Task<TResult> Create<TResult>(IWork<TResult> work)
        {
            return new Task<TResult>(work.Execute, TaskCreationOptions.LongRunning);
        }

        public Task Create(IWork work)
        {
            return new Task(work.Execute, TaskCreationOptions.LongRunning);
        }
    }
}