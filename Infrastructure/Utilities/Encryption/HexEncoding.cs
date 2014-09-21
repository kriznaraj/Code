using System;
using System.Globalization;
using System.Text;

namespace Controls.Encryption
{
    public class HexEncoding
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="asciiString">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <returns>
        /// </returns>
        public static string ASCIIToHex(string asciiString, Encoding encoding)
        {
            if (asciiString != null && asciiString.Length > 0)
            {
                return ByteArrayToHexString(encoding.GetBytes(asciiString));
            }

            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="bytes">
        /// </param>
        /// <returns>
        /// </returns>
        public static string ByteArrayToHexString(byte[] bytes)
        {
            string hexString = string.Empty;

            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += bytes[i].ToString("X2");
            }

            return hexString;
        }

        /// <summary>
        /// Converts 1 or 2 character string into equivalant byte value
        /// </summary>
        /// <param name="hex">
        /// 1 or 2 character string
        /// </param>
        /// <returns>
        /// byte
        /// </returns>
        public static byte HexStringToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
            {
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            }

            byte newByte = byte.Parse(hex, NumberStyles.HexNumber);
            return newByte;
        }

        /// <summary>
        /// Creates a byte array from the hexadecimal string. Each two characters are combined
        /// to create one byte. First two hexadecimal characters become first byte in returned array.
        /// Non-hexadecimal characters are ignored.
        /// </summary>
        /// <param name="hexString">
        /// string to convert to byte array
        /// </param>
        /// <param name="discarded">
        /// number of characters in string ignored
        /// </param>
        /// <returns>
        /// byte array, in the same left-to-right order as the hexString
        /// </returns>
        public static byte[] HexStringToByteArray(string hexString, out int discarded)
        {
            discarded = 0;
            string newString = string.Empty;
            char c;

            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                {
                    newString += c;
                }
                else
                {
                    discarded++;
                }
            }

            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0)
            {
                discarded++;
                newString = newString.Substring(0, newString.Length - 1);
            }

            int byteLength = newString.Length / 2;
            var bytes = new byte[byteLength];
            string hex;
            int j = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new string(new[] { newString[j], newString[j + 1] });
                bytes[i] = HexStringToByte(hex);
                j = j + 2;
            }

            return bytes;
        }

        /// <summary>
        /// Converts the Hex string format to ASCII string format.
        /// </summary>
        /// <param name="hexString">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <returns>
        /// </returns>
        public static string HexToASCII(string hexString, Encoding encoding)
        {
            int dis = 0;
            if (hexString != null && hexString.Length > 0)
            {
                return encoding.GetString(HexStringToByteArray(hexString, out dis));
            }

            return null;
        }

        /// <summary>
        /// Returns true is c is a hexadecimal digit (A-F, a-f, 0-9)
        /// </summary>
        /// <param name="c">
        /// Character to test
        /// </param>
        /// <returns>
        /// true if hex digit, false if not
        /// </returns>
        public static bool IsHexDigit(char c)
        {
            int numChar;
            int numA = Convert.ToInt32('A');
            int num1 = Convert.ToInt32('0');
            c = Char.ToUpper(c);
            numChar = Convert.ToInt32(c);
            if (numChar >= numA && numChar < (numA + 6))
            {
                return true;
            }

            if (numChar >= num1 && numChar < (num1 + 10))
            {
                return true;
            }

            return false;
        }

        #endregion Public Methods
    }
}