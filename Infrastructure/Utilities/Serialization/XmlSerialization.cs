using System.IO;
using System.Xml.Serialization;

namespace Controls.Serialization
{
    public class XmlSerialization : Serialization
    {
        protected override T Deserialize<T>(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T obj = default(T);
            stream.Position = 0;
            obj = (T)serializer.Deserialize(stream);
            return obj;
        }

        protected override MemoryStream Serialize<T>(T instance)
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, instance);
            return stream;
        }
    }
}