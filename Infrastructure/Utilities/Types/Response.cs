using System.Collections;
using System.Runtime.Serialization;

namespace Controls.Types
{
    [DataContract]
    public class Response : IResponse
    {
        public Response()
        {
            this.Params = new Hashtable();
        }

        [DataMember]
        public long[] ErrorCodes
        {
            get;
            set;
        }

        [DataMember]
        public IDictionary Params
        {
            get;
            set;
        }

        [DataMember]
        public ResultType Result
        {
            get;
            set;
        }
    }

    [DataContract]
    public class Response<T> : Response, IResponse<T>
    {
        [DataMember]
        public T Data
        {
            get;
            set;
        }
    }
}