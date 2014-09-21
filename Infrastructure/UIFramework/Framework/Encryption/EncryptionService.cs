using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controls.Encryption;
using Controls.Serialization;
using Controls.Framework.Interfaces;

namespace Controls.Framework
{
    public class EncryptionService : IEncryptionService
    {
        /// <summary>
        /// 
        /// </summary>
        private const string stringKey = "12345789";

        /// <summary>
        /// 
        /// </summary>
        IEncryption iEncrypion = null;

        /// <summary>
        /// 
        /// </summary>
        ISerialization iSerialization = null;

        /// <summary>
        /// 
        /// </summary>
        public EncryptionService(IEncryption encryption)
        {
            this.iEncrypion = encryption;
        }

        /// <summary>
        /// Retrun a encrypted string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            return this.iEncrypion.Encrypt<string>(value, stringKey);
        }

        /// <summary>
        /// Return a Decrypted a string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Decrypt(string value)
        {
            return this.iEncrypion.Decrypt<string>(value, stringKey);
        }
    }
}
