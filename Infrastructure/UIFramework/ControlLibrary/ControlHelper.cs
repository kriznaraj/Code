using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Controls.ControlLibrary
{
    internal static class ControlHelper
    {
        private const string _initVector = "tu89geji340t89u2";
        // This constant is used to determine the keysize of the encryption algorithm.
        private const int _keysize = 256;
        private const string _passPhrase = "dfgsdjgfksl";

        public static string Encrypt(string plainText)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(_initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(_passPhrase, null);
            byte[] keyBytes = password.GetBytes(_keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string cipherText)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(_initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(_passPhrase, null);
            byte[] keyBytes = password.GetBytes(_keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        //Helper method for filling
        public static string GetOptions(IEnumerable<object> srcList, string vMember, string dMember, string selectedValue = null, string[] disabled = null)
        {
            if (srcList == null)
            {
                return string.Empty;
            }
            
            string[] selecteds = null;
            StringBuilder options = new StringBuilder();

            var disabledList = disabled as IEnumerable<string>;
            
            if (selectedValue != null)
            {
                selecteds = selectedValue.Split(',').Select(m => m.Trim()).ToArray();
            }
            foreach (object item in srcList)
            {
                string value = getPropValue(item, vMember).ToString();
                if (disabledList != null && disabledList.Contains(value))
                {
                    options.AppendFormat("|{0},{1},disabled", value, getPropValue(item, dMember));
                }
                else if (selecteds != null && selecteds.Contains(value))
                {
                    options.AppendFormat("|{0},{1},selected", value, getPropValue(item, dMember));
                }
                else
                {
                    options.AppendFormat("|{0},{1}", value, getPropValue(item, dMember));
                }
            }

            return options.ToString();
        }

        public static string GetListItems(IEnumerable<object> srcList, string vMember, string[] param)
        {
            if (srcList == null)
            {
                return string.Empty;
            }
            string[] selecteds = null;

            StringBuilder options = new StringBuilder();

            foreach (object item in srcList)
            {
                if (item is string)
                {
                    options.AppendFormat("|{0}", item);
                }
                else
                {
                    //Assumed max supported parameters 5 + one Id
                    //First parameter ll be always taken as Id.
                    options.AppendFormat("|{0}", getPropValue(item, vMember).ToString());
                    options.AppendFormat(getPropValue(item, param));
                }
            }

            return options.ToString();
        }

        //For testin
        public static string getExtrString(string text)
        {
            return "EXTR" + text;
        }

        //Uses reflection to get values.
        private static string getPropValue(object src, string propName)
        {
            if (propName==null)
            {
                return src.ToString();
            }
            return src.GetType().GetProperty(propName).GetValue(src, null).ToString();
        }

        private static string getPropValue(object src, string[] props)
        {
            StringBuilder option = new StringBuilder();
            foreach (var prop in props)
            {
                option.Append("," + getPropValue(src, prop));
            }
            return option.ToString();
        }
    }    
}
