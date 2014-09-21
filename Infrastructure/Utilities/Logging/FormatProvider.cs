using System;
using System.Collections.Generic;
using Controls.Types;

namespace Controls.Logging
{
    public class FormatProvider : IFormatProvider
    {
        private readonly IFormatter fieldFormatter;
        private readonly IFormatter typeFormatter;
        private Dictionary<string, IFormatter> fieldFormatters;
        private Dictionary<string, IFormatter> typeFormatters;

        public FormatProvider(string defaultTypeFormatter, string defaultFieldFormatter, IDictionary<string, string> typeFormatter, IDictionary<string, string> fieldFormatter)
        {
            if (string.IsNullOrWhiteSpace(defaultTypeFormatter))
            {
                throw new ArgumentException("defaultTypeFormatter cannot be empty or null", defaultTypeFormatter);
            }

            if (string.IsNullOrWhiteSpace(defaultFieldFormatter))
            {
                throw new ArgumentException("defaultFieldFormatter cannot be empty or null", defaultFieldFormatter);
            }

            this.fieldFormatter = TypeFactory.CreateInstance<IFormatter>(defaultFieldFormatter);
            this.typeFormatter = TypeFactory.CreateInstance<IFormatter>(defaultTypeFormatter);
            this.fieldFormatters = new Dictionary<string, IFormatter>(StringComparer.OrdinalIgnoreCase);
            this.typeFormatters = new Dictionary<string, IFormatter>(StringComparer.OrdinalIgnoreCase);
            if (null != typeFormatter && typeFormatter.Count > 0)
            {
                FormatProvider.FillFormatters(typeFormatter, this.typeFormatters);
            }

            if (null != fieldFormatter && fieldFormatter.Count > 0)
            {
                FormatProvider.FillFormatters(fieldFormatter, this.fieldFormatters);
            }
        }

        public IFormatter GetFormatProvider<T>()
        {
            IFormatter formatter;
            if (false == this.typeFormatters.TryGetValue(typeof(T).FullName, out formatter))
            {
                formatter = this.typeFormatter;
            }

            return formatter;
        }

        public IFormatter GetFormatProvider(string key)
        {
            IFormatter formatter;
            if (false == this.fieldFormatters.TryGetValue(key, out formatter))
            {
                formatter = this.fieldFormatter;
            }

            return formatter;
        }

        private static void FillFormatters(IDictionary<string, string> dictionary, IDictionary<string, IFormatter> formatters)
        {
            foreach (var item in dictionary)
            {
                formatters.Add(item.Key, TypeFactory.CreateInstance<IFormatter>(item.Value));
            }
        }
    }
}