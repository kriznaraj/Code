using Controls.Compression;
using Controls.Configuration;
using Controls.Debugging;
using Controls.Encryption;
using Controls.ExceptionHandling;
using Controls.Logging;
using Controls.Printing;
using Controls.Random;
using Controls.Serialization;
using Controls.Threading;
using Controls.Threading.Provider;
using Controls.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Controls.Utilities
{
    public class UtilityProvider : IUtilityProvider
    {
        private readonly Lazy<ICompression> compression;
        private readonly DIContainer container;
        private readonly Lazy<IDebug> debug;
        private readonly Lazy<Serializer> defaultSerializer;
        private readonly Lazy<IInstrumentation> instrumentation;
        private readonly Lazy<ILogger> logger;
        private readonly Lazy<IPrintService> printService;
        private readonly Lazy<ISafeBlockProvider> safeBlockProvider;
        private readonly Lazy<IThreadPoolFactory> threadProvider;

        public UtilityProvider(DIContainer container, IConfigService configService, IMessageProvider messageProvider)
        {
            this.container = container;
            this.debug = new Lazy<IDebug>(() => DebugFactory.Create(configService), LazyThreadSafetyMode.ExecutionAndPublication);
            this.instrumentation = new Lazy<IInstrumentation>(() => InstrumentationFactory.Create(configService), LazyThreadSafetyMode.ExecutionAndPublication);
            this.compression = new Lazy<ICompression>(() => new Compression.Compression(), LazyThreadSafetyMode.ExecutionAndPublication);
            this.logger = new Lazy<ILogger>(() => LoggerFactory.Create(configService, messageProvider), LazyThreadSafetyMode.ExecutionAndPublication);
            this.safeBlockProvider = new Lazy<ISafeBlockProvider>(() => new SafeBlockProvider(configService, this.logger.Value), LazyThreadSafetyMode.ExecutionAndPublication);
            this.defaultSerializer = new Lazy<Serializer>(() => Serializer.Binary, LazyThreadSafetyMode.ExecutionAndPublication);
            this.printService = new Lazy<IPrintService>(() => PrintServiceFactory.Create(configService, this.logger.Value), LazyThreadSafetyMode.ExecutionAndPublication);
            this.threadProvider = new Lazy<IThreadPoolFactory>(() => new ThreadPoolFactory(configService), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IThreadPool CreateThreadPool(int noOfWorkers)
        {
            return this.threadProvider.Value.CreateThreadPool(noOfWorkers);
        }

        public ICompression GetCompression()
        {
            return this.compression.Value;
        }

        public IDebug GetDebug()
        {
            return this.debug.Value;
        }

        public IEncryption GetEncryptionProvider(Encryption.Encryption encryption)
        {
            switch (encryption)
            {
                case Encryption.Encryption.TripleDES: return new TripleDESEncryption(this.GetSerializer());
                case Encryption.Encryption.BlowFish: return new BlowFishEncryption(this.GetSerializer());
                case Encryption.Encryption.Rijndael: return new RijndaelEncryption(this.GetSerializer());
                default: throw new ArgumentException("encryption");
            }
        }

        public T GetInstance<T>()
        {
            return this.container.Get<T>();
        }

        public IInstrumentation GetInstrumentation()
        {
            return this.instrumentation.Value;
        }

        public IRNG<int> GetInt32RNG(int seed)
        {
            return new Int32RNGGenerator(seed);
        }

        public IRNG<long> GetInt64RNG(long seed)
        {
            return new Int64RNGGenerator(seed);
        }

        public ILogger GetLogger()
        {
            return this.logger.Value;
        }

        public IPrintService GetPrintService()
        {
            return this.printService.Value;
        }

        public ISafeBlockProvider GetSafeBlockProvider()
        {
            return this.safeBlockProvider.Value;
        }

        public ISerialization GetSerializer()
        {
            return this.GetSerializer(this.defaultSerializer.Value);
        }

        public ISerialization GetSerializer(Serializer serializer)
        {
            switch (serializer)
            {
                case Serializer.DataContract: return new DataContractSerialization();
                case Serializer.Json: return new JsonSerialization();
                case Serializer.BinaryNetSerializer: return new NetSerialization();
                case Serializer.BinaryProtobuf: return new ProtoBufSerialization();
                case Serializer.XML: return new XmlSerialization();
                case Serializer.Binary: return new BinarySerialization();
                default: throw new ArgumentException("serializer");
            }
        }
    }
}