using Controls.Configuration;
using Controls.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal static class PrintServiceFactory
    {
        internal static IPrintService Create(IConfigService configService, ILogger logger)
        {
            IPrintService printService = null;
            PrintServiceConfig printServiceConfig = configService.Get<PrintServiceConfig>("PrintManager", "ServiceConfig", null);
            if (null == printServiceConfig)
            {
                printService = new PrintServiceNull();
                logger.LogDebug("PrintService", "Print Service not configured. All request to print will throw exception");
            }
            else
            {
                printService = new PrintService(printServiceConfig, logger);
            }

            return printService;
        }
    }
}