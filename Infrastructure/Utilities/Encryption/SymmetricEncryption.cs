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
    public abstract class SymmetricEncryption : IEncryption
    {
        private readonly ISerialization serializer;

        /// <summary>
        /// Initialize new instance for SymmetricEncryption
        /// </summary>
        /// <param name="iSerialization">Serialization Instance</param>
        public SymmetricEncryption(ISerialization serializer)
        {
            this.serializer = serializer;
        }

        protected abstract int IVSize { get; }

        #region Public Methods

        /// <summary>
        /// Returns Decrypted data as Original object
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="input">Encrypted data</param>
        /// <param name="key">Password to decrypt string</param>
        /// <returns>Object</returns>
        public T Decrypt<T>(string input, string password) where T : class
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (var stream = this.Decrypt(ms, password, false))
                {
                    var data = Encoding.Unicode.GetBytes(input);
                    stream.Write(data, 0, data.Length);
                }

                ms.Seek(0, SeekOrigin.Begin);
                return this.serializer.Deserialize<T>(ms);
            }
        }

        /// <summary>
        ///  Returns Decrypted stream
        /// </summary>
        /// <param name="input">input stream to decrypt</param>
        /// <param name="key">key to use for decryption</param>
        /// <returns>return the decrypted stream</returns>
        public Stream Decrypt(Stream input, string key)
        {
            return this.Decrypt(input, key, true);
        }

        /// <summary>
        /// Returns Encrypted data as string
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="input">Object used for encryption</param>
        /// <param name="key">Password to encrypte object</param>
        /// <returns>Encrypted data as string</returns>
        public string Encrypt<T>(T input, string password)
        {
            using (Stream ms = this.serializer.Serialize<T>(input))
            {
                ms.Seek(0, SeekOrigin.Begin);
                using (var stream = this.Encrypt(ms, password, false))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Returns Encrypted data as stream
        /// </summary>
        /// <param name="input">stream to encrypt</param>
        /// <param name="key">password to use for encryption</param>
        /// <returns>returns the encryption stream</returns>
        public Stream Encrypt(Stream input, string key)
        {
            return this.Encrypt(input, key, true);
        }

        #endregion Public Methods

        protected abstract SymmetricAlgorithm CreateSymmetricEncryption(byte[] key, byte[] iv);

        private SymmetricAlgorithm CreateSymmetricEncryption(string key)
        {
            byte[] data = Encoding.Unicode.GetBytes(key);
            if (data != null && (data.Length == 32 || data.Length == 40 || data.Length == 48))
            {
                byte[] iv = new byte[this.IVSize];
                Array.Copy(data, data.Length - this.IVSize, iv, 0, this.IVSize);
                Array.Resize(ref data, data.Length - this.IVSize);

                return this.CreateSymmetricEncryption(data, iv);
            }
            else
            {
                throw new ArgumentException("Invalid Key Size", "key");
            }
        }

        private Stream Decrypt(Stream input, string key, bool closing)
        {
            if (closing)
            {
                return new CryptoStream(input, this.CreateSymmetricEncryption(key).CreateDecryptor(), CryptoStreamMode.Write);
            }
            else
            {
                return new BaseStreamNonDisposingCryptoStream(input, this.CreateSymmetricEncryption(key).CreateDecryptor(), CryptoStreamMode.Write);
            }
        }

        private Stream Encrypt(Stream input, string key, bool closing)
        {
            if (closing)
            {
                return new CryptoStream(input, this.CreateSymmetricEncryption(key).CreateEncryptor(), CryptoStreamMode.Read);
            }
            else
            {
                return new BaseStreamNonDisposingCryptoStream(input, this.CreateSymmetricEncryption(key).CreateEncryptor(), CryptoStreamMode.Read);
            }
        }
    }
}