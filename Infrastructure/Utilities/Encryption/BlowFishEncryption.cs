using Controls.Serialization;
using Controls.Types;
using System;
using System.IO;
using System.Text;

namespace Controls.Encryption
{
    /// <summary>
    /// Represents a BlowFish Encryption
    /// </summary>
    public class BlowFishEncryption : IEncryption
    {
        /// <summary>
        /// Returns Serialization Instance
        /// </summary>
        private ISerialization serializer;

        /// <summary>
        /// Initialize new instance for BlowFish
        /// </summary>
        /// <param name="iSerialization">Serialization Instance</param>
        public BlowFishEncryption(ISerialization serializer)
        {
            this.serializer = serializer;
        }

        public T Decrypt<T>(string input, string key) where T : class
        {
            Encoding encoding = Encoding.ASCII;
            T returnvalue = default(T);
            if (false == string.IsNullOrEmpty(input))
            {
                string asciiString = string.Empty;
                string hexDecrypt = this.DecryptHexString(key, input, encoding);
                asciiString = this.HexToAscii(hexDecrypt, encoding);

                if (typeof(T) != typeof(string))
                {
                    byte[] originalByteArray;
                    originalByteArray = Convert.FromBase64String(asciiString);
                    using (MemoryStream memoryStream = new MemoryStream(originalByteArray))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        returnvalue = this.serializer.Deserialize<T>(memoryStream);
                    }
                }
                else
                {
                    returnvalue = asciiString as T;
                }
            }
            return returnvalue;
        }

        public Stream Decrypt(Stream input, string key)
        {
            throw new NotSupportedException("Stream decryption/Encryption is not supported by BlowFish");
        }

        public string Encrypt<T>(T input, string key)
        {
            string returnValue = string.Empty;
            string inputString = string.Empty;
            Encoding encoding = Encoding.ASCII;

            if (typeof(T) != typeof(string))
            {
                byte[] inputBuffer;
                Stream stream = this.serializer.Serialize(input);
                inputBuffer = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(inputBuffer, 0, (int)stream.Length);
                inputString = Convert.ToBase64String(inputBuffer);
            }
            else
            {
                inputString = input.ToString();
            }

            if (false == string.IsNullOrEmpty(inputString))
            {
                string hexEncrypt = this.AsciiToHex(inputString, encoding);
                returnValue = this.EncryptHexString(key, hexEncrypt, encoding);
            }

            return returnValue;
        }

        public Stream Encrypt(Stream input, string key)
        {
            throw new NotSupportedException("Stream decryption/Encryption is not supported by BlowFish");
        }

        /// <summary>
        /// </summary>
        /// <param name="asciiString">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <returns>
        /// </returns>
        private string AsciiToHex(string asciiString, Encoding encoding)
        {
            return HexEncoding.ASCIIToHex(asciiString, encoding);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="hexString">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <returns>
        /// </returns>
        private string DecryptHexString(string key, string hexString, Encoding encoding)
        {
            BlowFish bfe = null;
            byte[] b = (encoding == Encoding.ASCII) ? Encoding.Default.GetBytes(key) : encoding.GetBytes(key);
            bfe = new BlowFish(b, 0, b.Length);

            int discarded = 0;

            byte[] enc = HexEncoding.HexStringToByteArray(hexString, out discarded);
            var dec = new byte[enc.Length];

            bfe.Decrypt(enc, 0, dec, 0, enc.Length);
            string str = (encoding == Encoding.ASCII) ? Encoding.Default.GetString(dec) : encoding.GetString(dec);
            return AsciiToHex(str, encoding);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="hexString">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <returns>
        /// </returns>
        private string EncryptHexString(string key, string hexString, Encoding encoding)
        {
            BlowFish bfe = null;
            byte[] b = (encoding == Encoding.ASCII) ? Encoding.Default.GetBytes(key) : encoding.GetBytes(key);
            bfe = new BlowFish(b, 0, b.Length);

            // Convert HexString to Ascii
            string asciiString = HexToAscii(hexString, encoding);
            if (asciiString != null)
            {
                int length = (((asciiString.Length / 8) * 8) != asciiString.Length)
                                 ? ((asciiString.Length / 8) + 1) * 8
                                 : asciiString.Length;

                string strPlain = asciiString.PadRight(length, ' ');
                byte[] plain = (encoding == Encoding.ASCII)
                                   ? Encoding.Default.GetBytes(strPlain)
                                   : encoding.GetBytes(strPlain);
                var enc = new byte[plain.Length];

                bfe.Encrypt(plain, 0, enc, 0, plain.Length);

                return HexEncoding.ByteArrayToHexString(enc);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns BlowFish instance
        /// </summary>
        /// <param name="key">Password to encrypte data</param>
        /// <returns>BlowFish instance</returns>
        private BlowFish GetBlowFish(string key)
        {
            byte[] password = Convert.FromBase64String(key);
            return new BlowFish(password, 0, password.Length);
        }

        /// <summary>
        /// </summary>
        /// <param name="hexString">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <returns>
        /// </returns>
        private string HexToAscii(string hexString, Encoding encoding)
        {
            return HexEncoding.HexToASCII(hexString, encoding);
        }
    }
}