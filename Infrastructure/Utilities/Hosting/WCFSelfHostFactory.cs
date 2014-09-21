using System;
using BallyTech.Infrastructure.Configuration;
using BallyTech.Infrastructure.Utilities;

namespace BallyTech.Infrastructure.Hosting
{
    public abstract class WCFSelfHostFactory : IServiceHostFactory
    {
        protected abstract String ConfigGroup { get; }

        protected abstract String ServiceName { get; }

        IServiceHost IServiceHostFactory.Create(IConfigService configService, IUtilityProvider utility)
        {
            var hostConfig = configService.Get<WCFSelfHostConfig>(ConfigGroup, ServiceName);
            int length = hostConfig.BaseAddresses.Length;
            Uri[] uris = new Uri[length];
            for (int init = 0; init < length; init++)
            {
                uris[init] = new Uri(hostConfig.BaseAddresses[init]);
            }
            return new WCFSelfHost(Type.GetType(hostConfig.Type), uris);
        }
    }
}