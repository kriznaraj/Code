using System;
using System.Runtime.Serialization;

namespace Controls.Types
{
    public sealed class DataException<TDetail> : Exception, IDataException
    {
        private readonly TDetail detail;

        public DataException(TDetail data)
        {
            this.detail = data;
        }

        public DataException(SerializationInfo info, StreamingContext context)
        {
            this.detail = (TDetail)info.GetValue("Detail", typeof(TDetail));
        }

        public TDetail Detail
        {
            get
            {
                return this.detail;
            }
        }

        public object GetObject()
        {
            return this.detail;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Detail", this.detail);
            base.GetObjectData(info, context);
        }
    }
}