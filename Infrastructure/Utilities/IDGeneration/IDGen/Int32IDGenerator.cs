using System;

namespace Controls.IDGeneration
{
    public class Int32IDGenertor : IDGenerator<Int32>
    {
        public Int32IDGenertor(String key, IRangeGenerator<Int32> rangeGenerator)
            : base(key, rangeGenerator)
        {
            this.SetRange();
        }

        public override int Next()
        {
            Int32 returnValue = 0;
            lock (this)
            {
                if (this.startRange >= this.endRange)
                {
                    this.SetRange();
                }

                returnValue = this.startRange++; ;
            }
            return returnValue;
        }
    }
}