using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Encryption
{
    public class NoEncryption : IEncryption
    {
        public NoEncryption()
        {
        }

        public T Decrypt<T>(string input, string key) where T : class
        {
            return input as T;
        }

        public Stream Decrypt(Stream input, string key)
        {
            return input;
        }

        public string Encrypt<T>(T input, string key)
        {
            return input as string;
        }

        public Stream Encrypt(Stream input, string key)
        {
            return input;
        }
    }
}