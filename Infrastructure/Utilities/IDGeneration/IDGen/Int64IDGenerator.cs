using System;

namespace Controls.IDGeneration
{
    public class Int64IDGenertor : IDGenerator<Int64>
    {
        public Int64IDGenertor(String key, IRangeGenerator<Int64> rangeGenerator)
            : base(key, rangeGenerator)
        {
        }

        public override Int64 Next()
        {
            Int64 returnValue = 0;

            lock (this)
            {
                if (this.startRange >= this.endRange)
                {
                    this.SetRange();
                }

                returnValue = this.startRange++;
            }

            return returnValue;
        }
    }
}