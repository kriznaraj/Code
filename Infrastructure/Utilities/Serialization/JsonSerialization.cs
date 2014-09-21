using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Serialization
{
    public class JsonSerialization : Serialization
    {
        protected override T Deserialize<T>(Stream stream)
        {
            using (JsonReader reader = new JsonTextReader(new StreamReader(stream)))
            {
                JsonSerializer ser = new JsonSerializer();
                return ser.Deserialize<T>(reader);
            }
        }

        protected override MemoryStream Serialize<T>(T instance)
        {
            MemoryStream ms = new MemoryStream();
            using (JsonTextWriter writer = new JsonTextWriter(new StreamWriter(ms)))
            {
                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(writer, instance);
            }

            return ms;
        }
    }
}