using Controls.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Printing
{
    internal class PrintManagerChannelFactory
    {
        private readonly string DefaultKey = "DEFAULT";
        private readonly Dictionary<string, PrintManagerChannelConfig> printManagerConfigDictionary;

        public PrintManagerChannelFactory(PrintServiceConfig printServiceConfig)
        {
            this.printManagerConfigDictionary = printServiceConfig.ChannelConfig.ToDictionary(o => o.Key, StringComparer.OrdinalIgnoreCase);
        }

        internal IPrintManager CreateManager(string key)
        {
            PrintManagerChannelConfig printMangerConfig = null;
            if (false == this.printManagerConfigDictionary.TryGetValue(key, out printMangerConfig))
            {
                printMangerConfig = this.printManagerConfigDictionary[DefaultKey];
            }

            return this.CreateManager(printMangerConfig.Binding, printMangerConfig.Address);
        }

        private IPrintManager CreateManager(Binding binding, EndpointAddress endpointAddress)
        {
            return new ChannelFactory<IPrintManager>(binding, endpointAddress).CreateChannel();
        }
    }
}