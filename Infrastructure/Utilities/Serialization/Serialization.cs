using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Serialization
{
    public abstract class Serialization : ISerialization
    {
        T ISerialization.Deserialize<T>(string data, Encoding encoding)
        {
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(data)))
            {
                return this.Deserialize<T>(ms);
            }
        }

        T ISerialization.Deserialize<T>(Stream stream)
        {
            return this.Deserialize<T>(stream);
        }

        Stream ISerialization.Serialize<T>(T instance)
        {
            return this.Serialize(instance);
        }

        string ISerialization.Serialize<T>(T instance, Encoding encoding)
        {
            return encoding.GetString(this.Serialize(instance).ToArray());
        }

        protected abstract T Deserialize<T>(Stream stream);

        protected abstract MemoryStream Serialize<T>(T instance);
    }
}