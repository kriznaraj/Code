using System;

namespace Controls.IDGeneration
{
    public class Int16IDGenertor : IDGenerator<Int16>
    {
        public Int16IDGenertor(String key, IRangeGenerator<Int16> rangeGenerator)
            : base(key, rangeGenerator)
        {
            this.SetRange();
        }

        public override Int16 Next()
        {
            Int16 returnValue = 0;
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