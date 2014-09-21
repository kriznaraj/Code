using System;

namespace Controls.Random
{
    public class Int64RNGGenerator : RGNGenerator<Int64>
    {
        public Int64RNGGenerator(Int64 seed)
            : base(seed)
        {
        }

        public override void SetSeed(Int64 seed)
        {
            mt_buffer[0] = (UInt64)(seed + Environment.TickCount) & 0xffffffffU;
            for (mt_index = 1; mt_index < N; ++mt_index)
            {
                mt_buffer[mt_index] = (69069 * mt_buffer[mt_index - 1]) & 0xffffffffU;
            }
        }

        public override Int64 GetNext()
        {
            return this.Next();
        }

        public override Int64 GetNext(Int64 min)
        {
            return this.Next(min);
        }

        public override Int64 GetNext(Int64 min, Int64 max)
        {
            return this.Next(min, max);
        }

        public Int64 Next()
        {
            return (Int64)this.Next64Bit();
        }

        public Int64 Next(Int64 maxValue)
        {
            if (maxValue <= 1)
            {
                if (maxValue < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return 0;
            }
            return (Int64)(this.NextRandom(maxValue));
        }

        public Int64 Next(Int64 minValue, Int64 maxValue)
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