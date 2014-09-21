using System;
using System.Threading;

namespace Controls.IDGeneration
{
    public abstract class GeneratorBase<T> : IIDGenerator<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public static readonly DateTime DefaultEpoch = new DateTime(2013, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        protected ushort Sequence;
        protected readonly DateTime Epoch;
        protected ulong LastTimeCountInMs;

        protected GeneratorBase(DateTime epoch)
        {
            Sequence = 0;
            Epoch = epoch;
            LastTimeCountInMs = CurrentTime();
        }

        public abstract T Next();

        protected void Update()
        {
            lock (this)
            {
                var timeInMs = CurrentTime();

                if (LastTimeCountInMs > timeInMs)
                    throw new InvalidOperationException(string.Format("Clock moved backwards. Refusing to generate id for {0} milliseconds", (LastTimeCountInMs - timeInMs)));

                //Change the logic to ensure that the Counter does not rollover
                //Counter = (ushort)((LastOxidizedInMs < timeInMs) ? 0 : Counter + 1);

                if (LastTimeCountInMs == timeInMs)
                {
                    //4096 is the max # of sequence numbers that can fit in 12bits.
                    //using a zero index, we cannot have a value larger than 4095
                    //if that point is reached, we reset the sequence and wait until the next
                    //millisecond to begin generating ID's
                    lock (this)
                    {
                        if (++Sequence > 4095)
                            Sequence = 0;
                    }

                    Console.WriteLine(Sequence);

                    if (Sequence == 0)
                    {
                        //Wait for next millisecond
                        timeInMs = TillNextMillis(LastTimeCountInMs);
                    }
                }
                else
                {
                    Sequence = 0;
                }

                LastTimeCountInMs = timeInMs;
            }
        }

        private ulong CurrentTime()
        {
            return (ulong)(DateTime.UtcNow - Epoch).TotalMilliseconds;
        }

        private ulong TillNextMillis(ulong lastTimestamp)
        {
            ulong currentTime = CurrentTime();

            SpinWait.SpinUntil(() => (currentTime = CurrentTime()) > lastTimestamp);

            return currentTime;
        }
    }

    public class UInt64TimeBasedIDGenerator : GeneratorBase<UInt64>
    {
        private readonly short _identifier;

        public UInt64TimeBasedIDGenerator(short identifier)
            : this(identifier, UInt64TimeBasedIDGenerator.DefaultEpoch)
        {
        }

        public UInt64TimeBasedIDGenerator(short identifier, DateTime epoch)
            : base(epoch)
        {
            _identifier = identifier;
        }

        public override UInt64 Next()
        {
            UInt64 tempVal = 0;
            lock (this)
            {
                Update();

                //verify our id portions are within expected ranges
                if (LastTimeCountInMs < 0 || LastTimeCountInMs > ulong.MaxValue)
                    throw new InvalidOperationException("UInt64 Generator time count is out of range");

                //_identifier cannot be larger than 2^10 or 1024.  (1023 when using zero index)
                if (_identifier < 0 || _identifier > 1023)
                    throw new InvalidOperationException("UInt64 Generator identifier (NodeID) is out of range");

                //Sequence cannot be larger than 2^12 or 4096.  (4095 when using zero index)
                if (Sequence < 0 || Sequence > 4095)
                    throw new InvalidOperationException("UInt64 Generator Sequence is out of range");

                Console.WriteLine("LastTimeCountInMs : " + LastTimeCountInMs);
                Console.WriteLine("_identifier : " + _identifier);
                Console.WriteLine("Sequence : " + Sequence);

                tempVal = (LastTimeCountInMs << 22) + (ulong)(_identifier << 10) + Sequence;
            }

            return tempVal;
        }
    }
}