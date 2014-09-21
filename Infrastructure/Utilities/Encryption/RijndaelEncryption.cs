using Controls.Serialization;
using Controls.Types;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Controls.Encryption
{
    /// <summary>
    /// Represents Rijndael Encryption
    /// </summary>
    public class RijndaelEncryption : SymmetricEncryption
    {
        private const int IV_SIZE = 16;

        /// <summary>
        /// Initialize new instance for RijndaelEncryption
        /// </summary>
        /// <param name="iSerialization">Serialization Instance</param>
        public RijndaelEncryption(ISerialization serializer)
            : base(serializer)
        {
        }

        protected override int IVSize
        {
            get { return RijndaelEncryption.IV_SIZE; }
        }

        protected override SymmetricAlgorithm CreateSymmetricEncryption(byte[] key, byte[] iv)
        {
            return new RijndaelManaged() { BlockSize = 128, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7, Key = key, IV = iv };
        }
    }
}