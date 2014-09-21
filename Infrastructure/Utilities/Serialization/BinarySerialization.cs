using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Controls.Serialization
{
    public class BinarySerialization : Serialization
    {
        protected override T Deserialize<T>(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }

        protected override MemoryStream Serialize<T>(T instance)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, instance);
            return stream;
        }
    }
}