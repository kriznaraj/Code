using BallyTech.Infrastructure.Configuration;
using BallyTech.Infrastructure.Utilities;

namespace BallyTech.Infrastructure.Hosting
{
    public interface ISingletonServiceHostFactory
    {
        IServiceHost Create(IConfigService configService, IUtilityProvider utility, object singletonInstance);
    }
}