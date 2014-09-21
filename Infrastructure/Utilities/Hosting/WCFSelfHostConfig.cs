using System;
using System.Runtime.Serialization;

namespace BallyTech.Infrastructure.Hosting
{
    [Serializable]
    public sealed class WCFSelfHostConfig : ISerializable
    {
        public string Type
        {
            get;
            set;
        }

        public string[] BaseAddresses
        {
            get;
            set;
        }

        public WCFSelfHostConfig()
        {
        }

        public WCFSelfHostConfig(string type, string[] baseAddresses)
        {
            this.Type = type;
            this.BaseAddresses = baseAddresses;
        }

        public WCFSelfHostConfig(SerializationInfo info, StreamingContext context)
        {
            this.Type = info.GetString("Type");
            this.BaseAddresses = (string[])info.GetValue("BaseAddresses", typeof(string[]));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Type", this.Type);
            info.AddValue("BaseAddresses", this.BaseAddresses);
        }
    }
}