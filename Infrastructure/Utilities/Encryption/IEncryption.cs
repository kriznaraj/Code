using System.IO;

namespace Controls.Encryption
{
    /// <summary>
    /// Represents encryption type
    /// </summary>
    public enum Encryption
    {
        /// <summary>
        /// Returns TripleDES Encryption
        /// </summary>
        TripleDES,

        /// <summary>
        /// Returns BlowFish Encryption
        /// </summary>
        BlowFish,

        /// <summary>
        /// Returns Rijndael Encryption
        /// </summary>
        Rijndael
    }

    /// <summary>
    /// Defines method for Encryption and decryption
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// Returns original object
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="input">Encrypted data as string</param>
        /// <param name="key">Password to decrypte, encrypted data</param>
        /// <returns>Original object</returns>
        T Decrypt<T>(string input, string key) where T : class;

        /// <summary>
        /// Decrypts the given stream
        /// </summary>
        /// <param name="output">Stream to decrypt</param>
        /// <param name="key">Key for the decryption</param>
        /// <returns>Returns the decrypted stream</returns>
        Stream Decrypt(Stream input, string key);

        /// <summary>
        /// Returns encrypted data as string
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="input">Object to use for encryption</param>
        /// <param name="key">Password to encrypte original object</param>
        /// <returns>Encrypted string</returns>
        string Encrypt<T>(T input, string key);

        /// <summary>
        /// Encrypts the Given Stream
        /// </summary>
        /// <param name="input">Stream to encrypt</param>
        /// <param name="key">Key to use for encryption</param>
        /// <returns>returns Encrypted Strean</returns>
        Stream Encrypt(Stream input, string key);
    }
}