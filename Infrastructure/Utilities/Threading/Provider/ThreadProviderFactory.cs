using Controls.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Threading.Provider
{
    internal class ThreadProviderFactory
    {
        private readonly IThreadProvider provider;

        internal ThreadProviderFactory(IConfigService configService)
        {
            ThreadProvider threadProvider = configService.Get<ThreadProvider>("Threading", "Provider", ThreadProvider.ThreadPool);
            switch (threadProvider)
            {
                case ThreadProvider.ThreadPool:
                    this.provider = ThreadPoolThreadProvider.Instance;
                    break;

                case ThreadProvider.UserThread:
                    this.provider = UserThreadProvider.Instance;
                    break;

                default:
                case ThreadProvider.None:
                    throw new ArgumentOutOfRangeException("Threading.Provider", "Invalid Threading Provider configured");
            }
        }

        internal IThreadProvider GetProvider()
        {
            return provider;
        }
    }
}