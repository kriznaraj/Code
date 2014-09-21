using Controls.Encryption;
using Controls.Serialization;
using Controls.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Controls.Configuration
{
    /// <summary>
    /// Defines set of method for storing and retrieving configuration object
    /// </summary>
    public class ConfigService : IConfigService
    {
        /// <summary>
        /// Manages configuration object
        /// </summary>
        private ConfigProvider configProvider;

        /// <summary>
        /// encryption/decryption key
        /// </summary>
        private string key;

        /// <summary>
        /// Initialize new instance for configuration object
        /// </summary>
        ///<param name="fileName">file name to use for reading configuration</param>
        ///<param name="key">Key to use for encryption/decryption</param>
        public ConfigService(string fileName)
        {
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = fileName;
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            this.configProvider = this.Configure(configuration);
        }

        /// <summary>
        /// Destructor to free all resource for this type
        /// </summary>
        ~ConfigService()
        {
            this.configProvider.Dispose();
            this.configProvider = null;
        }

        public T Get<T>(string typeKey, string key)
        {
            IEnumerable<T> values = this.configProvider.Read<T>(typeKey, key);
            if (values == null || values.Count() != 1)
            {
                throw new InvalidOperationException(string.Format("Data for given type {0} key {1} combination not found", typeKey, key));
            }

            return values.First();
        }

        public T Get<T>(string typeKey, string key, T defaultValue)
        {
            IEnumerable<T> values = this.configProvider.Read<T>(typeKey, key);
            if (values != null && values.Count() == 1)
            {
                defaultValue = values.First();
            }

            return defaultValue;
        }

        public IEnumerable<T> Get<T>(string typeKey)
        {
            return this.configProvider.Read<T>(typeKey);
        }

        public void Save<T>(string typeKey, string key, T value)
        {
            Dictionary<string, T> collection = new Dictionary<string, T>();
            collection.Add(key, value);
            this.configProvider.Write<T>(typeKey, collection);
        }

        public void Save<T>(string typeKey, IEnumerable<T> configuration, Func<T, string> keyFactory)
        {
            Dictionary<string, T> collection = new Dictionary<string, T>();
            foreach (var item in configuration)
            {
                collection.Add(keyFactory(item), item);
            }

            this.configProvider.Write(typeKey, collection);
        }

        private ConfigProvider Configure(System.Configuration.Configuration configuration)
        {
            var storageProviderType = Type.GetType(configuration.AppSettings.Settings["StorageProviderType"].Value, true);
            var storageProviderConnection = configuration.AppSettings.Settings["StorageProviderConnection"].Value;
            var storageProvider = TypeFactory.CreateInstance<IConfigStorageProvider>(storageProviderType, storageProviderConnection);
            var serializerType = Type.GetType(configuration.AppSettings.Settings["SerializerType"].Value, true);
            var serializer = TypeFactory.CreateInstance<ISerialization>(serializerType);
            bool encrypted;
            IEncryption encryptor;
            if (configuration.AppSettings.Settings["IsEncrypted"] != null && bool.TryParse(configuration.AppSettings.Settings["IsEncrypted"].Value, out encrypted) && encrypted)
            {
                var encryptionKey = configuration.AppSettings.Settings["Key"].Value;
                var encryptionType = Type.GetType(configuration.AppSettings.Settings["EncryptionType"].Value, true);
                encryptor = TypeFactory.CreateInstance<IEncryption>(encryptionType, serializer);
            }
            else
            {
                encryptor = new NoEncryption();
            }

            return new ConfigProvider(storageProvider, serializer, encryptor, this.key);
        }
    }
}