using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Framework
{
    [Serializable]
    public class ApplicationSettings : ISerializable
    {
        public string DefaultCulture { get; set; }

        public ApplicationSettings()
        {

        }

        public ApplicationSettings(SerializationInfo info, StreamingContext context)
        {
            this.DefaultCulture = (string)info.GetValue("DefaultCulture", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DefaultCulture", this.DefaultCulture, typeof(string));
        }
    }
}
