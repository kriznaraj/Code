using System;

namespace Controls.IDGeneration
{
    public interface IIDGenerator<out T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        T Next();
    }
}