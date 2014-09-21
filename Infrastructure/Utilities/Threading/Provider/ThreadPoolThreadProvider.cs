using Controls.Threading.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controls.Threading.Provider
{
    internal class ThreadPoolThreadProvider : IThreadProvider
    {
        private static readonly ThreadPoolThreadProvider instance = new ThreadPoolThreadProvider();

        private ThreadPoolThreadProvider()
        {
        }

        internal static ThreadPoolThreadProvider Instance
        {
            get { return ThreadPoolThreadProvider.instance; }
        }

        public Task<TResult> Create<TResult>(IWork<TResult> work)
        {
            return new Task<TResult>(work.Execute);
        }

        public Task Create(IWork work)
        {
            return new Task(work.Execute);
        }
    }
}