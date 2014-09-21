using Controls.Compression;
using Controls.Debugging;
using Controls.Encryption;
using Controls.ExceptionHandling;
using Controls.Logging;
using Controls.Printing;
using Controls.Random;
using Controls.Serialization;
using Controls.Threading;
using System;
using System.Text;

namespace Controls.Utilities
{
    public interface IUtilityProvider
    {
        IThreadPool CreateThreadPool(int noOfWorkers);

        ICompression GetCompression();

        IDebug GetDebug();

        IEncryption GetEncryptionProvider(Encryption.Encryption encryption = Encryption.Encryption.BlowFish);

        T GetInstance<T>();

        IInstrumentation GetInstrumentation();

        IRNG<Int32> GetInt32RNG(int seed);

        IRNG<Int64> GetInt64RNG(long seed);

        ILogger GetLogger();

        IPrintService GetPrintService();

        ISafeBlockProvider GetSafeBlockProvider();

        ISerialization GetSerializer();

        ISerialization GetSerializer(Serializer serilizer);
    }
}