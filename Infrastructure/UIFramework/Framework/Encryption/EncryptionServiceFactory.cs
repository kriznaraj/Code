using Controls.Encryption;
using Controls.Serialization;
using Controls.Framework.Interfaces;
using System;

namespace Controls.Framework
{
    public  class EncryptionServiceFactory
    {
        public static IEncryptionService Create(string encryption, string serialization)
        {
            Type encryptionType = Type.GetType(encryption);
            Type serializationType = Type.GetType(serialization);

            ISerialization iserialization = (ISerialization)Activator.CreateInstance(serializationType);

            IEncryption iEncryption = (IEncryption)Activator.CreateInstance(encryptionType, iserialization);

            IEncryptionService iEncryptionService = new EncryptionService(iEncryption);

            return iEncryptionService;
        }
    }
}
