using System;
using System.Collections.Generic;

namespace Controls.IDGeneration
{
    public class RangeGenerator<T> : IRangeGenerator<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        private readonly IIDRangeRepository idRangeRepository;
        private static object lockDB = new object();

        public RangeGenerator(IIDRangeRepository idRangeRepository)
        {
            this.idRangeRepository = idRangeRepository;
        }

        public KeyValuePair<T, T> NextNumberRange(string key)
        {
            IDRange idRange = this.idRangeRepository.GetNextRange(key);

            return new KeyValuePair<T, T>(
                TypeConvertor<T>.GetValue(
                    idRange.StartRange.ToString()),
                    TypeConvertor<T>.GetValue(idRange.EndRange.ToString()));
        }
    }
}