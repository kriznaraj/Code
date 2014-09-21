using System;
using System.Data;
using System.Data.SqlClient;
using BallyTech.Infrastructure.Types;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.SQLite;


namespace BallyTech.Infrastructure.Configuration
{
    public class SqliteStorageProvider : IConfigStorageProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private string connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SqliteStorageProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string SelectConfigQueryText
        {
            get { return "Select [Key],[Type],[Data] FROM [tUIConfiguration] WHERE [Key] = @Key"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SelectAllConfigQueryText
        {
            get { return "Select [Key],[Type],[Data] FROM [tUIConfiguration] WHERE [Type] = @Type"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string InsertConfigCommandText
        {
            get { return "INSERT INTO [tUIConfiguration] ([Key],[Type],[Data]) VALUES (@Key, @Type, @Data)"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UpdateConfigCommandText
        {
            get { return "UPDATE [tUIConfiguration] SET [Type] = @Type, [Data] = @Data WHERE [Key] = @Key"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbConnection GetConnection()
        {
            return new SQLiteConnection(this.connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dataParameter"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, IDbTransaction dbTransaction, CommandType commandType = CommandType.Text)
        {
            SQLiteCommand sqliteCommand = dbConnection.CreateCommand() as SQLiteCommand;
            sqliteCommand.CommandText = commandText;
            sqliteCommand.CommandType = commandType;
            sqliteCommand.Connection = dbConnection as SQLiteConnection;
            sqliteCommand.Parameters.AddRange(dataParameter);

            return sqliteCommand;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dataParameter"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, CommandType commandType = CommandType.Text)
        {
            SQLiteCommand sqliteCommand = dbConnection.CreateCommand() as SQLiteCommand;
            sqliteCommand.CommandText = commandText;
            sqliteCommand.CommandType = commandType;
            sqliteCommand.Connection = dbConnection as SQLiteConnection;
            if (dataParameter != null)
            {
                sqliteCommand.Parameters.AddRange(dataParameter);
            }

            return sqliteCommand;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        public IDbTransaction GetTransaction(IDbConnection dbConnection)
        {
            return dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(IDbCommand command)
        {
            bool returnValue = false;

            if (command != null)
            {
                command.Prepare();
                command.ExecuteNonQuery();
                returnValue = true;
            }

            return returnValue;
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IEnumerable<string> ExecuteQuery(IDbCommand command)
        {
            List<string> data = new List<string>();
            DataTable dataTable = new DataTable();

            try
            {
                using (SQLiteCommand sqliteCommand = command as SQLiteCommand)
                {
                    SQLiteDataReader reader = sqliteCommand.ExecuteReader();
                    dataTable.Load(reader);
                }

                foreach (DataRow item in dataTable.Rows)
                {
                    string serializedData;
                    if ((serializedData = item.Field<string>("Data")) != null && serializedData != string.Empty)
                    {
                        data.Add(serializedData);
                    }
                }


            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        ~SqliteStorageProvider()
        {
            this.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDataParameter GetParameter(string parameterName, object value)
        {
            return new SQLiteParameter(parameterName, value);
        }
    }
}
