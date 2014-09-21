using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.Types
{
    /// <summary>
    /// Rijndael Encryption class to support both stream and string using given encoding and key size
    /// </summary>
    public sealed class Rijndael
    {
        private const int BLOCK_SIZE = 128;
        private const CipherMode CIPHER_MODE = CipherMode.CBC;
        private const int KEY_SIZE = 128;
        private const PaddingMode PADDING_MODE = PaddingMode.PKCS7;
        private readonly Encoding encoding;
        private readonly RijndaelManaged rijndaelManaged;

        /// <summary>
        /// Creates a new instance rijndael encryption with the given cofiguration
        /// </summary>
        /// <param name="key">Key to use for the encryption</param>
        /// <param name="salt">Salt to use for the encryption</param>
        /// <param name="encoding">Encoding to use for converting the string key and input to byte for ecnryption and to convert the data after decryption</param>
        /// <param name="keySize">Size of the key to use for the encryption</param>
        /// <param name="blockSize">Block size to use for Encryption</param>
        /// <param name="cipherMode">Cipher mode to use for encryption</param>
        /// <param name="paddingMode">Padding mode to use for encryption</param>
        public Rijndael(string key, byte[] salt, Encoding encoding = null, int keySize = KEY_SIZE, int blockSize = BLOCK_SIZE, CipherMode cipherMode = CIPHER_MODE, PaddingMode paddingMode = PADDING_MODE)
        {
            this.encoding = encoding ?? Encoding.UTF8;
            this.rijndaelManaged = new RijndaelManaged
            {
                KeySize = keySize,
                BlockSize = blockSize,
                Mode = cipherMode,
                Padding = paddingMode,
                Key = this.encoding.GetBytes(key),
                IV = salt,
            };
        }

        private enum Mode
        {
            Encrypt,
            Decrypt
        }

        /// <summary>
        /// Decrypts the given stream
        /// </summary>
        /// <param name="input">Stream to decrypt</param>
        /// <returns>Returns the decrypted Stream</returns>
        public Stream Decrypt(Stream input)
        {
            return this.InternalDecrypt(input);
        }

        /// <summary>
        /// Decrypts the given String
        /// </summary>
        /// <param name="input">Base64 Encoded String to decrypt</param>
        /// <returns>Returns the Decrypted String</returns>
        public string Decrypt(string input)
        {
            return this.encoding.GetString(this.InternalDecrypt(new MemoryStream(Convert.FromBase64String(input))).ToArray());
        }

        /// <summary>
        /// Encrypts the given stream
        /// </summary>
        /// <param name="input">Stream to encrypt</param>
        /// <returns>Returns the Encrypted Stream</returns>
        public Stream Encrypt(Stream input)
        {
            return this.InternalEncrypt(input);
        }

        /// <summary>
        /// Encrypts the given String
        /// </summary>
        /// <param name="input">String to encrypt</param>
        /// <returns>Returns the Encrypted String in Base64 Encoding</returns>
        public string Encrypt(string input)
        {
            return Convert.ToBase64String(this.InternalEncrypt(new MemoryStream(this.encoding.GetBytes(input))).ToArray());
        }

        private ICryptoTransform GetCryptoTransform(Mode mode)
        {
            switch (mode)
            {
                case Mode.Encrypt:
                    return this.rijndaelManaged.CreateEncryptor();

                case Mode.Decrypt:
                    return this.rijndaelManaged.CreateDecryptor();

                default:
                    throw new NotImplementedException();
            }
        }

        private MemoryStream InternalDecrypt(Stream inputStream)
        {
            MemoryStream memoryStream = new MemoryStream();

            using (CryptoStream stream = new BaseStreamNonDisposingCryptoStream(inputStream, this.GetCryptoTransform(Mode.Decrypt), CryptoStreamMode.Read))
            {
                stream.CopyStream(memoryStream);
            }

            return memoryStream;
        }

        private MemoryStream InternalEncrypt(Stream inputStream)
        {
            MemoryStream memoryStream = new MemoryStream();

            using (CryptoStream stream = new BaseStreamNonDisposingCryptoStream(memoryStream, this.GetCryptoTransform(Mode.Encrypt), CryptoStreamMode.Write))
            {
                inputStream.CopyStream(stream);
            }

            return memoryStream;
        }
    }
}