using System;

namespace Controls.IDGeneration
{
    public class IDRangeId : IComparable<IDRangeId>, IEquatable<IDRangeId>
    {
        private readonly String key;

        public String Key
        {
            get
            {
                return this.key;
            }
        }

        public IDRangeId(String key)
        {
            this.key = key;
        }

        public bool Equals(IDRangeId other)
        {
            return this.key == other.key;
        }

        public int CompareTo(IDRangeId other)
        {
            return this.key.CompareTo(other.key);
        }
    }
}