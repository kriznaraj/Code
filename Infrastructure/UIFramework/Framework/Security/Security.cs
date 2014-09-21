using System;
using System.Security.Cryptography;
using System.Text;

namespace Controls.Framework
{
    /*This Class used to create and Verify the Token using MD5 crypto.*/
    public static class Security
    {
        public static string GetHash(MD5 crypto, string value)
        {
            byte[] hashByte = crypto.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < hashByte.Length; i++)
            {
                sBuilder.Append(hashByte[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static bool VerifyHash(MD5 crypto, string input, string value)
        {
            string hashOfInput = GetHash(crypto, value);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
