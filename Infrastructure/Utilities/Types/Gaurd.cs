using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Types
{
    /// <summary>
    /// Class hold the extension methods that is used for Guard Check
    /// </summary>
    public static class Guard
    {
        private const string PARAMETER = "Parameter";

        /// <summary>
        /// Checks if the value is greater than or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void GreaterThanEqualsZero(this Decimal value, string paramName, long errorCode, string message)
        {
            if (value < 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is greater than or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void GreaterThanEqualsZero(this Int32 value, string paramName, long errorCode, string message)
        {
            if (value < 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is greater than or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void GreaterThanEqualsZero(this Int64 value, string paramName, long errorCode, string message)
        {
            if (value < 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is greater than zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void GreaterThanZero(this Decimal value, string paramName, long errorCode, string message)
        {
            if (value <= 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is greater than zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void GreaterThanZero(this Int64 value, string paramName, long errorCode, string message)
        {
            if (value <= 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is greater than zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void GreaterThanZero(this Int32 value, string paramName, long errorCode, string message)
        {
            if (value <= 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the collection is not null and empty
        /// </summary>
        /// <typeparam name="T">Type of the object in list</typeparam>
        /// <param name="value">object collection</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void IsListEmptyOrNull<T>(this ICollection<T> value, string paramName, long errorCode, string message)
        {
            if (null == value || value.Count <= 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// checks whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void IsStringEmptyNullOrWhitespace(this string value, string paramName, long errorCode, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the string is null, empty, and Length is between given range
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="minLength">Minimum length of the string. Should be greater than zero</param>
        /// <param name="maxLength">Maximum Length of the string. Should be Less than or equal to Int32.MaxValue</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void IsStringOfLength(this string value, int minLength, int maxLength, string paramName, long errorCode, string message)
        {
            value.IsStringEmptyNullOrWhitespace(paramName, errorCode, message);
            if (minLength <= value.Length && maxLength >= value.Length)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the string is null, empty, and Length is less than given length
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="maxLength">Maximum Length of the string. Should be Less than or equal to Int32.MaxValue</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void IsStringOfMaxLength(this string value, int maxLength, string paramName, long errorCode, string message)
        {
            value.IsStringOfLength(1, maxLength, paramName, errorCode, message);
        }

        /// <summary>
        /// Checks if the string is null, empty, and Length is greater than given length
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="minLength">Minimum length of the string. Should be greater than zero</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void IsStringOfMinLength(this string value, int minLength, string paramName, long errorCode, string message)
        {
            value.IsStringOfLength(minLength, Int32.MaxValue, paramName, errorCode, message);
        }

        /// <summary>
        /// Checks if the value is less than zero or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void LessThanEqualsZero(this Decimal value, string paramName, long errorCode, string message)
        {
            if (value > 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is less than zero or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void LessThanEqualsZero(this Int32 value, string paramName, long errorCode, string message)
        {
            if (value > 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is less than zero or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void LessThanEqualsZero(this Int64 value, string paramName, long errorCode, string message)
        {
            if (value > 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is less than zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void LessThanZero(this Decimal value, string paramName, long errorCode, string message)
        {
            if (value >= 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is less than zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void LessThanZero(this Int32 value, string paramName, long errorCode, string message)
        {
            if (value >= 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is less than zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void LessThanZero(this Int64 value, string paramName, long errorCode, string message)
        {
            if (value >= 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is non zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void NonZero(this Int32 value, string paramName, long errorCode, string message)
        {
            if (value == 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is non zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void NonZero(this Int64 value, string paramName, long errorCode, string message)
        {
            if (value == 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is non zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void NonZero(this Decimal value, string paramName, long errorCode, string message)
        {
            if (value == 0)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }

        /// <summary>
        /// Checks if the value is not null
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="errorCode">error code to be used if exception is thrown</param>
        /// <param name="message">message to be associated with the error code</param>
        public static void NotNull<T>(this T value, string paramName, long errorCode, string message) where T : class
        {
            if (null == value)
            {
                throw new ModuleException(errorCode, message, new Pair<string, string>(PARAMETER, paramName));
            }
        }
    }
}