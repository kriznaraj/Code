namespace Controls.IDGeneration
{
    using System;
    using System.Data;
    using System.Reflection;

    #region TypeConvertor for the Boxing and Unboxing

    /// <summary>
    /// TypeConvertor Class
    /// </summary>
    /// <typeparam name="T">
    /// Type for this TypeConvertor is Used
    /// </typeparam>
    public static class TypeConvertor<T>
    {
        #region Constants and Fields

        /// <summary>
        /// </summary>
        private static readonly TryParseDelegate<T> InvocationMethod;

        /// <summary>
        /// </summary>
        private static readonly Func<object, T> TypeCastMethod;

        /// <summary>
        /// </summary>
        private static readonly Func<string, T> TypeConvertMethod;

        #endregion Constants and Fields

        #region Constructors and Destructors

        /// <summary>
        /// Type initializer for the TypeConvertor
        /// </summary>
        static TypeConvertor()
        {
            MethodInfo methodInfo = typeof(T).GetMethod(
                "TryParse", new[] { typeof(string), typeof(T).MakeByRefType() });
            if (methodInfo != null)
            {
                InvocationMethod =
                    Delegate.CreateDelegate(typeof(TryParseDelegate<T>), methodInfo) as TryParseDelegate<T>;
            }

            string typeConvertMethodName = "GenricParseValue";
            string typeCastMethodName = "TypeCastValue";

            if (typeof(T) == typeof(decimal))
            {
                typeCastMethodName = "TypeCastDecimalValue";
            }
            else if (typeof(T) == typeof(DateTimeOffset?))
            {
                typeCastMethodName = "TypeCastNullDateTimeOffsetValue";
            }
            else if (typeof(T) == typeof(DateTimeOffset))
            {
                typeCastMethodName = "TypeCastDateTimeOffsetValue";
            }
            else if (typeof(T) == typeof(DateTime?))
            {
                typeCastMethodName = "TypeCastNullDateTimeValue";
            }
            else if (typeof(T) == typeof(DateTime))
            {
                typeCastMethodName = "TypeCastDateTimeValue";
            }
            else if (typeof(T) == typeof(bool))
            {
                typeConvertMethodName = "BoolParseValue";
            }
            else if (typeof(T) == typeof(string))
            {
                typeConvertMethodName = "StringParseValue";
            }

            MethodInfo typeCastMethodInfo = typeof(TypeConvertor<T>).GetMethod(
                typeCastMethodName, BindingFlags.Static | BindingFlags.NonPublic);

            MethodInfo typeConvertMethodInfo = typeof(TypeConvertor<T>).GetMethod(
                typeConvertMethodName, BindingFlags.Static | BindingFlags.NonPublic);

            if (typeCastMethodInfo != null)
            {
                TypeCastMethod = Delegate.CreateDelegate(typeof(Func<object, T>), typeCastMethodInfo) as Func<object, T>;
            }

            if (typeConvertMethodInfo != null)
            {
                TypeConvertMethod =
                    Delegate.CreateDelegate(typeof(Func<string, T>), typeConvertMethodInfo) as Func<string, T>;
            }
        }

        #endregion Constructors and Destructors

        #region Delegates

        /// <summary>
        /// </summary>
        /// <param name="Value">
        /// </param>
        /// <param name="retValue">
        /// </param>
        /// <typeparam name="K">
        /// </typeparam>
        private delegate bool TryParseDelegate<K>(string Value, out K retValue);

        #endregion Delegates

        #region Public Methods

        /// <summary>
        /// Gets the Enum Value for the given string
        /// </summary>
        /// <param name="name">
        /// Name of the enumeration
        /// </param>
        /// <returns>
        /// Enum Value
        /// </returns>
        public static T GetEnum(string name)
        {
            if (false == Enum.IsDefined(typeof(T), name))
            {
                throw new ArgumentNullException(String.Format("Enum Not Found {0}", name));
            }

            return (T)Enum.Parse(typeof(T), name);
        }

        /// <summary>
        /// Returns the Value from the DataRow to the requested Type
        /// </summary>
        /// <param name="dataRow">
        /// DataRow object
        /// </param>
        /// <param name="index">
        /// Index of the Columns
        /// </param>
        /// <returns>
        /// Value
        /// </returns>
        public static T GetValue(DataRow dataRow, int index)
        {
            T retValue = default(T);
            if (dataRow != null && dataRow[index] != null && dataRow[index] != DBNull.Value)
            {
                retValue = (T)dataRow[index];
            }

            return retValue;
        }

        /// <summary>
        /// Returns the Value from the DataRow to the requested Type
        /// </summary>
        /// <param name="dataRow">
        /// DataRow object
        /// </param>
        /// <param name="columnName">
        /// Name of the Column
        /// </param>
        /// <returns>
        /// Value
        /// </returns>
        public static T GetValue(DataRow dataRow, string columnName)
        {
            T retValue = default(T);
            if (dataRow != null && dataRow[columnName] != null && dataRow[columnName] != DBNull.Value)
            {
                retValue = (T)dataRow[columnName];
            }

            return retValue;
        }

        /// <summary>
        /// Converts the Value from the object to the requested Type
        /// </summary>
        /// <param name="value">
        /// object Value
        /// </param>
        /// <returns>
        /// Value
        /// </returns>
        public static T GetValue(object value)
        {
            return TypeCastMethod(value);
        }

        /// <summary>
        /// Converts the Value to string
        /// </summary>
        /// <param name="value">
        /// Value
        /// </param>
        /// <returns>
        /// string Value
        /// </returns>
        public static string GetValue(T value)
        {
            string retValue = string.Empty;
            if (value != null)
            {
                retValue = value.ToString();
            }

            return retValue;
        }

        /// <summary>
        /// Converts the string to value
        /// </summary>
        /// <param name="value">
        /// string Value
        /// </param>
        /// <returns>
        /// Value
        /// </returns>
        public static T GetValue(string value)
        {
            T retValue = default(T);
            if (false == string.IsNullOrEmpty(value))
            {
                retValue = TypeConvertMethod.Invoke(value);
            }

            return retValue;
        }

        #endregion Public Methods

        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        private static bool BoolParseValue(string value)
        {
            bool retValue;
            bool.TryParse("1" == value ? "True" : value, out retValue);
            return retValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        private static T GenricParseValue(string value)
        {
            T retValue = default(T);
            if (false == string.IsNullOrEmpty(value))
            {
                InvocationMethod.Invoke(value, out retValue);
            }

            return retValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        private static string StringParseValue(string value)
        {
            return value;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        private static DateTimeOffset TypeCastDateTimeOffsetValue(object value)
        {
            DateTimeOffset retValue = default(DateTimeOffset);
            if (value != null && value != DBNull.Value)
            {
                if (value is DateTime)
                {
                    retValue = (DateTime)value;
                }
                else
                {
                    retValue = (DateTimeOffset)value;
                }
            }

            return retValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        private static DateTime TypeCastDateTimeValue(object value)
        {
            DateTime retValue = default(DateTime);
            if (value != null && value != DBNull.Value)
            {
                if (value is DateTimeOffset)
                {
                    retValue = ((DateTimeOffset)value).DateTime;
                }
                else
                {
                    retValue = (DateTime)value;
                }
            }

            return retValue;
        }

        private static decimal TypeCastDecimalValue(object value)
        {
            decimal retValue = 0;
            if (null != value && value != DBNull.Value)
            {
                retValue = (typeof(decimal) == value.GetType()) ? (decimal)value : TypeConvertor<decimal>.GetValue(value.ToString());
            }

            return retValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        private static DateTimeOffset? TypeCastNullDateTimeOffsetValue(object value)
        {
            DateTimeOffset? retValue = default(DateTimeOffset?);
            if (value != null && value != DBNull.Value)
            {
                if (value is DateTime?)
                {
                    retValue = (DateTime?)value;
                }
                else if (value is DateTime)
                {
                    retValue = (DateTime)value;
                }
                else
                {
                    retValue = (DateTimeOffset?)value;
                }
            }

            return retValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        private static DateTime? TypeCastNullDateTimeValue(object value)
        {
            DateTime? retValue = default(DateTime?);
            if (value != null && value != DBNull.Value)
            {
                if (value is DateTimeOffset?)
                {
                    retValue = ((DateTimeOffset?)value).Value.DateTime;
                }
                else if (value is DateTimeOffset)
                {
                    retValue = ((DateTimeOffset)value).DateTime;
                }
                else
                {
                    retValue = (DateTime?)value;
                }
            }

            return retValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        private static T TypeCastValue(object value)
        {
            T retValue = default(T);
            if (value != null && value != DBNull.Value)
            {
                retValue = (T)value;
            }

            return retValue;
        }

        #endregion Methods
    }

    #endregion TypeConvertor for the Boxing and Unboxing
}