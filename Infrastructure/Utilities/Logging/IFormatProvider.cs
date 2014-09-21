namespace Controls.Logging
{
    /// <summary>
    /// Interface to be implemented by the format providers
    /// </summary>
    public interface IFormatProvider
    {
        /// <summary>
        /// Get the formatter to be used for the type
        /// </summary>
        /// <typeparam name="T">Type of the param</typeparam>
        /// <returns>returns the formatter</returns>
        IFormatter GetFormatProvider<T>();

        /// <summary>
        /// Get the format provider for the given string key
        /// </summary>
        /// <param name="key">Key to get the formatter</param>
        /// <returns>Returns the Format provider for the key</returns>
        IFormatter GetFormatProvider(string key);
    }
}