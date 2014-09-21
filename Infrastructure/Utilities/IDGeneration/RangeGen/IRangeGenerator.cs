using System;
using System.Collections.Generic;

namespace Controls.IDGeneration
{
    public interface IRangeGenerator<T>
         where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        KeyValuePair<T, T> NextNumberRange(string key);
    }
}