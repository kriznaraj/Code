using System;

namespace Controls.Random
{
    public class Int32RNGGenerator : RGNGenerator<Int32>
    {
        public Int32RNGGenerator(Int32 seed)
            : base(seed)
        {
        }

        public override void SetSeed(Int32 seed)
        {
            mt_buffer_32[0] = (UInt32)(seed + Environment.TickCount) & 0xffffffffU;

            for (mt_index_32 = 1; mt_index_32 < N; mt_index_32++)
            {
                mt_buffer_32[mt_index_32] =
                    ((UInt32)1812433253 * (mt_buffer_32[mt_index_32 - 1] ^ (mt_buffer_32[mt_index_32 - 1] >> 30)) + mt_index_32);
            }
        }

        public override int GetNext()
        {
            return this.Next();
        }

        public override int GetNext(int min)
        {
            return this.Next(min);
        }

        public override int GetNext(int min, int max)
        {
            return this.Next(min, max);
        }

        public Int32 Next()
        {
            return (Int32)this.Next32Bit();
        }

        public int Next(int maxValue)
        {
            if (maxValue <= 1)
            {
                if (maxValue < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return 0;
            }

            return (Int32)(this.NextRandom(maxValue));
        }

        public int Next(int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (maxValue == minValue)
            {
                return minValue;
            }
            else
            {
                return this.Next(maxValue - minValue) + minValue;
            }
        }
    }
}