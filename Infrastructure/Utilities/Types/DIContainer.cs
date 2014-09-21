using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Controls.Types
{
    /// <summary>
    /// Represents a class that provides a dictionary to register and get instances for a given type.
    /// </summary>
    public class DIContainer
    {
        private readonly IDictionary<String, Object> namedProviders = new ConcurrentDictionary<String, Object>();

        public static DIContainer Instance
        {
            get;
            private set;
        }

        public static DIContainer Init()
        {
            if (null != Instance)
                throw new InvalidOperationException("Container already initialized");

            Instance = new DIContainer();

            return Instance;
        }

        /// <summary>
        /// To retrieve an entity instance for the specified key
        /// </summary>
        /// <typeparam name="T">Type of the instance</typeparam>
        /// <returns>Instance of the entity</returns>
        public T Get<T>()
        {
            object retVal;
            string key = typeof(T).FullName;
            if (false == this.namedProviders.TryGetValue(key, out retVal))
            {
                throw new KeyNotFoundException("The given key '" + key + "' is not found in the container");
            }

            return (T)retVal;
        }

        /// <summary>
        /// To save an entity instance against a key.
        /// </summary>
        /// <typeparam name="T">Type of the instance</typeparam>
        /// <param name="instance">Instance of the entity</param>
        public void Register<T>(T instance)
        {
            namedProviders.Add(typeof(T).FullName, instance);
        }
    }
}