using BallyTech.Infrastructure.Configuration;
using BallyTech.Infrastructure.Utilities;

namespace BallyTech.Infrastructure.Hosting
{
    public interface IServiceHostFactory
    {
        IServiceHost Create(IConfigService configService, IUtilityProvider utility);
    }
}