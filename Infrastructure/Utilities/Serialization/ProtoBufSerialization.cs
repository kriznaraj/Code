using System.IO;

namespace Controls.Serialization
{
    public class ProtoBufSerialization : Serialization
    {
        protected override T Deserialize<T>(Stream stream)
        {
            stream.Position = 0;
            T obj = ProtoBuf.Serializer.Deserialize<T>(stream);
            return obj;
        }

        protected override MemoryStream Serialize<T>(T instance)
        {
            MemoryStream stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize(stream, instance);
            return stream;
        }
    }
}