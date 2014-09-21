using Controls.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintManagerProviderFactory
    {
        private readonly PrintManagerChannelFactory factory;
        private readonly IPrintServiceNotificationProvider provider;
        private readonly ServiceHost servcieHost;

        public PrintManagerProviderFactory(PrintServiceConfig printServiceConfig, PrintManagerChannelFactory factory, IPrintServiceNotificationProvider provider)
        {
            this.factory = factory;
            this.provider = provider;
            this.servcieHost = new ServiceHost(new PrintManagerCallback(this.provider), new Uri(printServiceConfig.ListenUri));
        }

        public IPrintManagerProvider GetPrintManager(string key, PrintMode printMode)
        {
            switch (key.ToUpperInvariant())
            {
                case "PM":
                    {
                        switch (printMode)
                        {
                            case PrintMode.Sync:
                                return new PrintManagerSyncProvider(this.factory, this.provider);

                            case PrintMode.Async:
                                return new PrintManagerAsyncProvider(this.factory, this.provider, this.provider);

                            default:
                                throw new NotSupportedException("printMode");
                        }
                    }

                default:
                    throw new NotSupportedException(string.Format("Key = {0}", key));
            }
        }

        public void StartListeners()
        {
            this.servcieHost.Open();
        }

        public void StopListeners()
        {
            this.servcieHost.Close();
        }
    }
}