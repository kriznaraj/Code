using BallyTech.Infrastructure.Compression;
using BallyTech.Infrastructure.Configuration;
using BallyTech.Infrastructure.Debugging;
using BallyTech.Infrastructure.DI;
using BallyTech.Infrastructure.Logging;
using BallyTech.Infrastructure.Random;
using BallyTech.Infrastructure.Serialization;

namespace BallyTech.Infrastructure
{
    public static class Configurator
    {
        public static IUtil Configure(string configFilePath)
        {
            LoadConfigService(container, configFilePath);
            var configService = container.Get<IConfigService>();

            LoadDebug(container, configService);
            LoadInstrumentation(container, configService);
            LoadLogger(container, configService);
            LoadRNG(container);
            LoadSerializarion(container);
            LoadCompression(container);
            LoadUtil(container);
        }

        private static void LoadConfigService(IDIContainer container)
        {
            var configService = ConfigServiceFactory.Create();
            container.RegistryInstance<IConfigService>(configService, TypeInstancingMode.Singleton);
        }

        private static void LoadDebug(IDIContainer container, IConfigService configService)
        {
            var debug = DebugFactory.Create(configService);
            container.RegistryInstance<IDebug>(debug);
        }

        private static void LoadInstrumentation(IDIContainer container, IConfigService configService)
        {
            var instrumentation = InstrumentationFactory.Create(configService);
            container.RegistryInstance<IInstrumentation>(instrumentation);
        }

        private static void LoadLogger(IDIContainer container, IConfigService configService)
        {
            var logger = LoggerFactory.Create(configService, null);
            container.RegistryInstance<ILogger>(logger);
        }

        private static void LoadRNG(IDIContainer container)
        {
            container.RegisterType<Int32RNGGenerator>(RNGType.Int32.ToString(), TypeInstancingMode.Instance);
            container.RegisterType<Int64RNGGenerator>(RNGType.Int64.ToString(), TypeInstancingMode.Instance);
        }

        private static void LoadSerializarion(IDIContainer container)
        {
            container.RegisterType<ISerialization, NetSerialization>("EncryptionSerializer", TypeInstancingMode.Singleton);
            container.RegisterType<ISerialization, XmlSerialization>("DefaultSerializer", TypeInstancingMode.Singleton);
        }

        private static void LoadCompression(IDIContainer container)
        {
            BallyTech.Infrastructure.Compression.Compression compression = new BallyTech.Infrastructure.Compression.Compression();
            container.RegistryInstance<ICompression>(compression, TypeInstancingMode.Singleton);
        }

        private static void LoadUtil(IDIContainer container)
        {
            var util = new Util(container) as IUtil;
            container.RegistryInstance<IUtil>(util);
        }
    }
}