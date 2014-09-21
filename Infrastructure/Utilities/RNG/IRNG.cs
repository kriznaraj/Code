using System;

namespace Controls.Random
{
    public interface IRNG<T> where T : IEquatable<T>, IComparable<T>, IComparable
    {
        T GetNext();

        T GetNext(T min);

        T GetNext(T min, T max);

        void SetSeed(T seed);
    }
}