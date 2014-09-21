using System.IO;
using System.Runtime.Serialization;

namespace Controls.Serialization
{
    public class DataContractSerialization : Serialization
    {
        protected override T Deserialize<T>(Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            stream.Position = 0;
            T obj = (T)serializer.ReadObject(stream);
            //DataContractSerializer.
            return obj;
        }

        protected override MemoryStream Serialize<T>(T instance)
        {
            MemoryStream stream = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, instance);

            return stream;
        }
    }
}