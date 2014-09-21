using Controls.Serialization;
using Controls.Types;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Controls.Encryption
{
    /// <summary>
    /// Represents TipleDES Encryption
    /// </summary>
    public class TripleDESEncryption : SymmetricEncryption
    {
        private const int IV_SIZE = 8;

        /// <summary>
        /// Initialize new instance for TripleDESEncryption
        /// </summary>
        /// <param name="iSerialization">Serialization Instance</param>
        public TripleDESEncryption(ISerialization serializer)
            : base(serializer)
        {
        }

        protected override int IVSize
        {
            get { return TripleDESEncryption.IV_SIZE; }
        }

        protected override SymmetricAlgorithm CreateSymmetricEncryption(byte[] key, byte[] iv)
        {
            return new TripleDESCryptoServiceProvider() { BlockSize = 64, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7, Key = key, IV = iv };
        }
    }
}