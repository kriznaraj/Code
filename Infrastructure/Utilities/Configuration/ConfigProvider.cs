using Controls.Encryption;
using Controls.Serialization;
using Controls.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;

namespace Controls.Configuration
{
    internal class ConfigProvider : IDisposable
    {
        /// <summary>
        /// Entryption Provider
        /// </summary>
        private readonly IEncryption encryption;

        private readonly string key;

        /// <summary>
        /// Serialize object
        /// </summary>
        private ISerialization serializer;

        /// <summary>
        /// DatabaseManager object
        /// </summary>
        private IConfigStorageProvider storageProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="storageProvider"></param>
        /// <param name="serializer"></param>
        internal ConfigProvider(IConfigStorageProvider storageProvider, ISerialization serializer, IEncryption encryption, string key)
        {
            this.storageProvider = storageProvider;
            this.encryption = encryption;
            this.key = key;
            this.serializer = serializer;
        }

        /// <summary>
        /// Dispose all instance of this type
        /// </summary>
        public void Dispose()
        {
            this.serializer = null;
            this.storageProvider = null;
        }

        /// <summary>
        /// Returns Configuration object
        /// </summary>
        /// <typeparam name="T">Type of the configuration object</typeparam>
        /// <param name="_name">Key name to find configuration object</param>
        /// <returns>Configuration object</returns>
        internal IList<T> Read<T>(string type, string key = null)
        {
            IEnumerable<string> data = null;
            IList<T> configuration = new List<T>(); // default(T);
            if ((data = this.ExecuteSelectCommand(type, key)) != null)
            {
                foreach (var item in data)
                {
                    configuration.Add(this.serializer.Deserialize<T>(new MemoryStream(Convert.FromBase64String(this.encryption.Decrypt<string>(item, this.key)))));
                }
            }
            return configuration;
        }

        /// <summary>
        /// Sets the object.
        /// </summary>
        /// <typeparam name="T">Type of the configuration object</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns><c>true</c> if configuration has been saved successfully, <c>false</c> otherwise.</returns>
        internal bool Write<T>(string type, IEnumerable<KeyValuePair<string, T>> configuration)
        {
            Dictionary<string, string> configs = new Dictionary<string, string>();
            foreach (var item in configuration)
            {
                configs.Add(item.Key, this.encryption.Encrypt(Convert.ToBase64String(item.Value.ToBytes<T>(this.serializer)), this.key));
            }
            return this.ExecuteNonQuery(type, configs);
        }

        /// <summary>
        /// Returns True if configuration object saved successfully else false
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>
        /// <param name="key">Key name to find configuration object</param>
        /// <param name="typeName">FullyQualified name for the object</param>
        /// <param name="data">Serialized data</param>
        /// <returns>True/False</returns>
        private bool ExecuteNonQuery(string typeName, IEnumerable<KeyValuePair<string, string>> data)
        {
            bool retFlag = true;
            using (IDbConnection dbConnection = this.storageProvider.GetConnection())
            {
                dbConnection.Open();
                using (IDbTransaction dbTransaction = this.storageProvider.GetTransaction(dbConnection))
                {
                    try
                    {
                        foreach (var item in data)
                        {
                            IDataParameter[] dataParameter = new[] { this.storageProvider.GetParameter("Key", item.Key),
                                                                     this.storageProvider.GetParameter("Type",typeName),
                                                                     this.storageProvider.GetParameter("Data",item.Value)};
                            var ret = this.ExecuteSelectCommand(typeName, item.Key);
                            var commandText = ret == null || ret.Count() == 0 ? this.storageProvider.InsertConfigCommandText : this.storageProvider.UpdateConfigCommandText;
                            IDbCommand dbCommand = this.GetCommand(commandText, dbConnection, dataParameter, dbTransaction);
                            this.storageProvider.ExecuteNonQuery(dbCommand);
                        }

                        dbTransaction.Commit();
                    }
                    catch (SqlCeException CeEx)
                    {
                        retFlag = false;
                        dbTransaction.Rollback();
                        throw CeEx;
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }
            return retFlag;
        }

        /// <summary>
        /// Returns datatable for a key
        /// </summary>
        /// <param name="key">Key name to find configuration object</param>
        /// <returns>DataTable</returns>
        private IEnumerable<string> ExecuteSelectCommand(string typeName, string key = null)
        {
            IEnumerable<string> data = null;
            using (IDbConnection dbConnection = this.storageProvider.GetConnection())
            {
                dbConnection.Open();
                using (IDbTransaction dbTransaction = this.storageProvider.GetTransaction(dbConnection))
                {
                    string commandText;
                    IDataParameter[] dataParameter;
                    if (key == null)
                    {
                        dataParameter = new[] { this.storageProvider.GetParameter("Type", typeName) };
                        commandText = this.storageProvider.SelectAllConfigQueryText;
                    }
                    else
                    {
                        dataParameter = new[] { this.storageProvider.GetParameter("Type", typeName),
                                                this.storageProvider.GetParameter("Key", key) };
                        commandText = this.storageProvider.SelectConfigQueryText;
                    }

                    IDbCommand dbCommand = this.GetCommand(commandText, dbConnection, dataParameter, dbTransaction);
                    data = this.storageProvider.ExecuteQuery(dbCommand);
                    dbConnection.Close();
                }
            }
            return data;
        }

        /// <summary>
        /// Returns Command object
        /// </summary>
        /// <param name="storedProcedureName">StoredProcedure name</param>
        /// <param name="dataParameter">Parameter collection</param>
        /// <param name="dbConnection">Database connection instance</param>
        /// <param name="dbTransaction">Database transaction instance</param>
        /// <returns>Command object</returns>
        private IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, IDbTransaction dbTransaction)
        {
            return this.storageProvider.GetCommand(commandText, dbConnection, dataParameter, dbTransaction, CommandType.Text);
        }
    }
}