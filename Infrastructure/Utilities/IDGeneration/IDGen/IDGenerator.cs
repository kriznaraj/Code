using System;
using System.Collections.Generic;

namespace Controls.IDGeneration
{
    public abstract class IDGenerator<T> : IIDGenerator<T>
         where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        protected T startRange;
        protected T endRange;
        protected string key;
        protected IRangeGenerator<T> rangeGenerator;

        public IDGenerator(String key, IRangeGenerator<T> rangeGenerator)
        {
            this.key = key.ToUpperInvariant();
            this.rangeGenerator = rangeGenerator;
            this.startRange = default(T);
            this.endRange = default(T);
        }

        protected virtual void SetRange()
        {
            KeyValuePair<T, T> range = rangeGenerator.NextNumberRange(key);
            this.startRange = range.Key;
            this.endRange = range.Value;
        }

        public abstract T Next();
    }
}