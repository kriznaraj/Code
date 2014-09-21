using System.IO;
using System.Text;

namespace Controls.Serialization
{
    /// <summary>
    /// Represents encryption type
    /// </summary>
    public enum Serializer
    {
        DataContract,
        XML,
        Binary,
        BinaryNetSerializer,
        BinaryProtobuf,
        Json
    }

    /// <summary>
    /// Interface to be implemented by the serailizers
    /// </summary>
    public interface ISerialization
    {
        /// <summary>
        /// Deserializes the input string to the object
        /// </summary>
        /// <typeparam name="T">Type of the Object to deserialize</typeparam>
        /// <param name="data">string input</param>
        /// <param name="encoding">Encoding to use for converting to string</param>
        /// <returns>returns the deserialized object</returns>
        T Deserialize<T>(string data, Encoding encoding);

        /// <summary>
        /// Deserializes the input stream to the object
        /// </summary>
        /// <typeparam name="T">Type of the Object to deserialize</typeparam>
        /// <param name="stream">Input Stream</param>
        /// <returns>returns the deserialized object</returns>
        T Deserialize<T>(Stream stream);

        /// <summary>
        /// Serializes the object into stream
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="instance">Object to be serialized</param>
        /// <returns>returns the serialized object in stream</returns>
        Stream Serialize<T>(T instance);

        /// <summary>
        /// Serializes the object into string using the given encoding
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="instance">Object to be serialized</param>
        /// <param name="encoding">Encoding to use for converting the byte to string</param>
        /// <returns>returns the serialized object as string</returns>
        string Serialize<T>(T instance, Encoding encoding);
    }
}