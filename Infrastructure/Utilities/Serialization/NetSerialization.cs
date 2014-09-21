using System;
using System.IO;

namespace Controls.Serialization
{
    public class NetSerialization : Serialization
    {
        public static void RegisterType(Type[] type)
        {
            NetSerializer.Serializer.Initialize(type);
        }

        protected override T Deserialize<T>(Stream stream)
        {
            stream.Position = 0;
            T obj = (T)NetSerializer.Serializer.Deserialize(stream);
            return obj;
        }

        protected override MemoryStream Serialize<T>(T instance)
        {
            MemoryStream stream = new MemoryStream();
            NetSerializer.Serializer.Serialize(stream, instance);
            return stream;
        }
    }
}