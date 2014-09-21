using Controls.Configuration;
using Controls.Threading.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading
{
    public class ThreadPoolFactory : IThreadPoolFactory
    {
        private readonly ThreadProviderFactory factory;

        public ThreadPoolFactory(IConfigService configService)
        {
            this.factory = new ThreadProviderFactory(configService);
        }

        public IThreadPool CreateThreadPool(int noOfWorkers)
        {
            return new ThreadPool(this.factory.GetProvider(), noOfWorkers);
        }
    }
}