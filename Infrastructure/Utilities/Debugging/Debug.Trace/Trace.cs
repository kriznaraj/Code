using System.Diagnostics;
using System.Runtime.Serialization;

namespace Controls.Debugging
{
    public class Trace<T> : ITrace<T> where T : ISerializable
    {
        private int id;
        private T data;
        private TraceEventType traceEventType;

        public Trace(int id, T data, TraceEventType traceEventType)
        {
            this.id = id;
            this.data = data;
            this.traceEventType = traceEventType;
        }

        public int ID
        {
            get
            {
                return this.id;
            }
        }

        public T Data
        {
            get
            {
                return this.data;
            }
        }

        public TraceEventType TraceEventType
        {
            get
            {
                return this.traceEventType;
            }
        }
    }
}