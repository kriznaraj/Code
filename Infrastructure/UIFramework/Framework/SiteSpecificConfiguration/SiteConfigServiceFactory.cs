using Controls.Framework.Interfaces;

namespace Controls.Framework
{
    public class SiteConfigServiceFactory
    {
        public static ISiteConfigProvider Create()
        {            
            return new SiteConfigProvider();
        }
    }
}
