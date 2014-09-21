using System;
using System.Collections.Generic;

namespace Controls.Types
{
    public abstract class TypeLocator<T, TLookUpKey, TTypeConfig>
    {
        private readonly Dictionary<TLookUpKey, T> instances = new Dictionary<TLookUpKey, T>();

        public T GetInstance(TLookUpKey key)
        {
            return this.instances[key];
        }

        public void ResolveType(TLookUpKey key, TTypeConfig config, Func<TTypeConfig, Type> typeResolver)
        {
            var type = typeResolver(config);
            var instance = TypeFactory.CreateInstance<T>(type);
            this.instances.Add(key, instance);
        }
    }
}