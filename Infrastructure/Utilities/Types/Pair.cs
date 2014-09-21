using System.Runtime.Serialization;

namespace Controls.Types
{
    [DataContract]
    public class Pair<T, K>
    {
        [DataMember]
        public T One { get; set; }

        [DataMember]
        public K Two { get; set; }

        public Pair(T one, K two)
        {
            this.One = one;
            this.Two = two;
        }

        public Pair()
        {
        }
    }
}