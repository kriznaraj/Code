using System;
using System.Collections.Concurrent;

namespace Controls.IDGeneration
{
    public class IDGeneratorFactory<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        private static readonly ConcurrentDictionary<string, IIDGenerator<T>> idGenerators =
            new ConcurrentDictionary<string, IIDGenerator<T>>();

        public static IIDGenerator<T> Create(string key, IRangeGenerator<T> rg)
        {
            IIDGenerator<T> idGen = null;

            if (false == idGenerators.TryGetValue(key, out idGen))
            {
                switch (typeof(T).FullName)
                {
                    case "System.Int64":
                        {
                            idGen = (IIDGenerator<T>)new Int64IDGenertor(key, (IRangeGenerator<Int64>)rg);
                            break;
                        }

                    case "System.Int32":
                        {
                            idGen = (IIDGenerator<T>)new Int32IDGenertor(key, (IRangeGenerator<Int32>)rg);
                            break;
                        }

                    case "System.Int16":
                        {
                            idGen = (IIDGenerator<T>)new Int16IDGenertor(key, (IRangeGenerator<Int16>)rg);
                            break;
                        }
                }

                if (!idGenerators.TryAdd(key, idGen))
                    throw new Exception("Cannot create ID Generator.");
            }

            return idGen;
        }
    }
}