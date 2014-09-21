using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace Controls.Configuration
{
    /// <summary>
    /// Defines set of methods for Database operation
    /// </summary>
    public class SqlStorageProvider : IConfigStorageProvider
    {
        /// <summary>
        /// Database connection string
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Select stored procedure name
        /// </summary>
        public string SelectConfigQueryText
        {
            get
            {
                return "SELECT [Key],[Type],[Data] FROM [tConfiguration] WHERE [Type] = @Type AND [Key] = @Key";
            }
        }

        public string SelectAllConfigQueryText
        {
            get
            {
                return "SELECT [Key],[Type],[Data] FROM [tConfiguration] WHERE [Type] = @Type";
            }
        }

        /// <summary>
        /// Insert stored procedure name
        /// </summary>
        public string InsertConfigCommandText
        {
            get
            {
                return "INSERT INTO [tConfiguration] ([Key],[Type],[Data]) VALUES (@Key, @Type, @Data)";
            }
        }

        /// <summary>
        /// Update stored procedure name
        /// </summary>
        public string UpdateConfigCommandText
        {
            get
            {
                return "UPDATE [tConfiguration] SET [Data] = @Data WHERE [Key] = @Key AND [Type] = @Type";
            }
        }

        /// <summary>
        /// Initialize new instance for sql Database
        /// </summary>
        /// <param name="connectionString">Connection string for Sql Database</param>
        public SqlStorageProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Returns new SqlConnection object
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public IDbConnection GetConnection()
        {
            return new SqlCeConnection(this.connectionString);
        }

        /// <summary>
        /// Returns SqlTransaction object
        /// </summary>
        /// <param name="dbConnection">SqlDatabase connection object</param>
        /// <returns>SqlTransaction object</returns>
        public IDbTransaction GetTransaction(IDbConnection dbConnection)
        {
            return dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Returns True if command executed successfully else false
        /// </summary>
        /// <param name="command">SqlCommand object</param>
        /// <returns>True/False</returns>
        public bool ExecuteNonQuery(IDbCommand command)
        {
            command.Prepare();
            command.ExecuteNonQuery();
            return true;
        }

        /// <summary>
        /// Returns datatable for the key
        /// </summary>
        /// <param name="command">SqlCommand object for select command</param>
        /// <returns>DataTable</returns>
        public IEnumerable<string> ExecuteQuery(IDbCommand command)
        {
            List<string> data = new List<string>();
            DataTable outPutTable = new DataTable();
            try
            {
                SqlCeCommand sqlceCommand = command as SqlCeCommand;
                new SqlCeDataAdapter(sqlceCommand).Fill(outPutTable);
                foreach (DataRow item in outPutTable.Rows)
                {
                    string serializedData;
                    if ((serializedData = item.Field<string>("Data")) != null && serializedData != string.Empty)
                    {
                        data.Add(serializedData);
                    }
                }
                command.Dispose();
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            return data;
        }

        /// <summary>
        /// Returns SqlCommand object
        /// </summary>
        /// <param name="commandText">StoredProcedure name</param>
        /// <param name="dbConnection">SqlConnection object</param>
        /// <param name="dataParameter">SqlParameter Collection</param>
        /// <param name="dbTransaction">SqlTransaction object</param>
        /// <param name="commandType">Command Type</param>
        /// <returns>SqlCommand object</returns>
        public IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, IDbTransaction dbTransaction, CommandType commandType = CommandType.Text)
        {
            SqlCeCommand sqlceCommand = dbConnection.CreateCommand() as SqlCeCommand;
            sqlceCommand.CommandText = commandText;
            sqlceCommand.CommandType = commandType;
            sqlceCommand.Connection = dbConnection as SqlCeConnection;
            sqlceCommand.Transaction = dbTransaction as SqlCeTransaction;
            foreach (var item in dataParameter)
            {
                sqlceCommand.Parameters.Add(item);
            }
            return sqlceCommand;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDataParameter GetParameter(string parameterName, object value)
        {
            return new SqlCeParameter(parameterName, value);
        }

        /// <summary>
        /// Dispose all instance of this type
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Destructor to free all resource for this type
        /// </summary>
        ~SqlStorageProvider()
        {
            this.Dispose();
        }
    }
}