using System;
using System.Collections.Generic;

namespace Controls.Configuration
{
    /// <summary>
    /// Defines set of method for storing and retrieving configuration object
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Retrieves a configuration for a given type with the key provided
        /// </summary>
        /// <typeparam name="T">Type of the object to be retrieved</typeparam>
        /// <param name="typeKey">Type key will be used as the table Name</param>
        /// <param name="key">Key of the object</param>
        /// <returns>Return the object</returns>
        T Get<T>(string typeKey, string key);

        /// <summary>
        /// Retrieves a configuration for a given type with the key provided if found else returns the default value provided
        /// </summary>
        /// <typeparam name="T">Type of the object to be retrieved</typeparam>
        /// <param name="typeKey">Type key will be used as the table Name</param>
        /// <param name="key">Key of the object</param>
        /// <param name="defaultValue">default value if key not found</param>
        /// <returns>Return the object</returns>
        T Get<T>(string typeKey, string key, T defaultValue);

        /// <summary>
        /// Saves the configuration into the given table with key and value
        /// </summary>
        /// <typeparam name="T">Type of the object to be retrieved</typeparam>
        /// <param name="typeKey">Type key will be used as the table Name</param>
        /// <param name="key">Key of the object</param>
        /// <param name="value">Value to be stored</param>
        void Save<T>(string typeKey, string key, T value);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T">Type of the object to be retrieved</typeparam>
        /// <param name="typeKey">Type key will be used as the table Name</param>
        /// <param name="configuration">Collection of the object to be stored</param>
        /// <param name="keyFactory">Factory to be used for generating the key</param>
        void Save<T>(string typeKey, IEnumerable<T> configuration, Func<T, string> keyFactory);

        /// <summary>
        /// Retrieves a configuration for a given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeKey">>Type key will be used as the table Name</param>
        /// <returns>All the object in the given configuration</returns>
        IEnumerable<T> Get<T>(string typeKey);
    }
}