using System;
using System.Security.Cryptography;

namespace Controls.Random
{
    public abstract class RGNGenerator<T> : IRNG<T> where T : IEquatable<T>, IComparable<T>, IComparable
    {
        private RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        protected const int N = 624;
        private const int M = 397;
        protected short mt_index;
        protected UInt64[] mt_buffer = new UInt64[N];

        protected UInt32[] mt_buffer_32 = new UInt32[N];
        protected ushort mt_index_32 = N + 1;

        private byte[] _uint32Buffer = new byte[4];
        private byte[] _uint64Buffer = new byte[8];
        protected UInt32[] mag01 = new UInt32[] { 0, (UInt32)0x9908b0df };
        protected T seed;

        public abstract T GetNext();

        public abstract T GetNext(T min);

        public abstract T GetNext(T min, T max);

        public abstract void SetSeed(T seed);

        /* initializing the array with a NONZERO seed */

        public RGNGenerator(T seed)
        {
            this.seed = seed;
            this.SetSeed(seed);
        }

        public Int64 Next64Bit()
        {
            _rng.GetBytes(_uint64Buffer);
            Int64 _rand = BitConverter.ToInt64(_uint64Buffer, 0) & 0x7FFFFFFFFFFFFFFF;
            return _rand;
        }

        public Int32 Next32Bit()
        {
            _rng.GetBytes(_uint32Buffer);
            Int32 _rand = BitConverter.ToInt32(_uint32Buffer, 0) & 0x7FFFFFFF;
            return _rand;
        }

        public int BetweenRange64(Int64 number)
        {
            int i = 1;
            while (number >> i++ > 0) ;
            return i;
        }

        public int BetweenRange32(Int32 number)
        {
            int i = 1;
            while (number >> i++ > 0) ;
            return i;
        }

        public Int64 NextRandom(Int64 maxval)
        {
            UInt64 randomNumber = this.Random();

            int _bits = BetweenRange64(maxval);

            UInt64 i = (UInt64)Int64.MaxValue << 64 - _bits;

            UInt64 j = i | randomNumber;

            UInt64 k = j << 64 - _bits;

            UInt64 l = k >> 64 - _bits;

            if ((Int64)l > maxval)
                return (Int64)l % maxval;
            else
                return (Int64)l;
        }

        public Int32 NextRandom(Int32 maxval)
        {
            UInt32 _num = this.GenRand_Int32();
            if (_num > Int32.MaxValue)
                _num = _num >> 1;
            UInt32 randomNumber = _num;

            int _bits = BetweenRange32(maxval);

            UInt32 i = (UInt32)Int32.MaxValue << 32 - _bits;

            UInt32 j = i | randomNumber;

            UInt32 k = j << 32 - _bits;

            UInt32 l = k >> 32 - _bits;

            if ((Int32)l > maxval)
                return (Int32)l % maxval;
            else
                return (Int32)l;
        }

        protected UInt64 Random()
        {
            UInt64 s;
            if (mt_index >= N)
            {
                short i = 0;

                for (; i < N - M; ++i)
                {
                    s = (mt_buffer[i] & 0x80000000) | (mt_buffer[i + 1] & 0x7FFFFFFF);
                    mt_buffer[i] = mt_buffer[i + M] ^ (s >> 1) ^ ((s & 1) * 0x9908B0DF);
                }
                for (; i < N - 1; ++i)
                {
                    s = (mt_buffer[i] & 0x80000000) | (mt_buffer[i + 1] & 0x7FFFFFFF);
                    mt_buffer[i] = mt_buffer[i - (N - M)] ^ (s >> 1) ^ ((s & 1) * 0x9908B0DF);
                }

                s = (mt_buffer[623] & 0x80000000) | (mt_buffer[0] & 0x7FFFFFFF);
                mt_buffer[N - 1] = mt_buffer[M - 1] ^ (s >> 1) ^ ((s & 1) * 0x9908B0DF);

                mt_index = 0;
            }

            s = mt_buffer[mt_index++];
            s ^= s >> 11;
            s ^= s << 7 & 0x9d2c5680;
            s ^= s << 15 & 0xefc60000;
            s ^= s >> 18;
            return s;
        }

        protected UInt32 GenRand_Int32()
        {
            UInt32 y;

            if (mt_index_32 >= N)
            {
                Int16 kk;

                if (mt_index_32 == N + 1)
                    Init_GenRand(5489);

                for (kk = 0; kk < N - M; kk++)
                {
                    y = ((mt_buffer_32[kk] & (UInt32)0x80000000) | (mt_buffer_32[kk + 1] & (UInt32)0x7fffffff)) >> 1;
                    mt_buffer_32[kk] = mt_buffer_32[kk + M] ^ mag01[mt_buffer_32[kk + 1] & 1] ^ y;
                }
                for (; kk < N - 1; kk++)
                {
                    y = ((mt_buffer_32[kk] & (UInt32)0x80000000) | (mt_buffer_32[kk + 1] & (UInt32)0x7fffffff)) >> 1;
                    mt_buffer_32[kk] = mt_buffer_32[kk + (M - N)] ^ mag01[mt_buffer_32[kk + 1] & 1] ^ y;
                }
                y = ((mt_buffer_32[N - 1] & (UInt32)0x80000000) | (mt_buffer_32[0] & (UInt32)0x7fffffff)) >> 1;
                mt_buffer_32[N - 1] = mt_buffer_32[M - 1] ^ mag01[mt_buffer_32[0] & 1] ^ y;

                mt_index_32 = 0;
            }

            y = mt_buffer_32[mt_index_32++];

            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680;
            y ^= (y << 15) & 0xefc60000;
            y ^= (y >> 18);

            return y;
        }

        protected void Init_GenRand(UInt32 s)
        {
            mt_buffer_32[0] = s;

            for (mt_index_32 = 1; mt_index_32 < N; mt_index_32++)
            {
                mt_buffer_32[mt_index_32] =
                    ((UInt32)1812433253 * (mt_buffer_32[mt_index_32 - 1] ^ (mt_buffer_32[mt_index_32 - 1] >> 30)) + mt_index_32);
            }
        }
    }
}