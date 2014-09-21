using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using System.Collections.Generic;


namespace BallyTech.UI.Web.ResourceManager
{
    public class SqliteDataProvider : IDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private string connectionString;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SqliteDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbParameter[] GetParameter(List<KeyValuePair<string, object>> parameterList)
        {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>(parameterList.Count);
            parameterList.ForEach(o => parameters.Add(new SQLiteParameter(o.Key, o.Value)));
            return parameters.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public string InsertQuery
        {
            get { return "INSERT INTO [tLiterals] ([Key],[Type],[Data]) VALUES (@Key, @Type, @Data)"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UpdateQuery
        {
            get { return "UPDATE [tLiterals] SET [Type] = @Type, [Data] = @Data WHERE [Key] = @Key"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbConnection GetConnection
        {
            get { return new SQLiteConnection(this.connectionString); }
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
        public DbDataReader ExecuteQuery(IDbCommand command)
        {
            string data = string.Empty;
            string serializedData = string.Empty;
            DataTable dataTable = new DataTable();

            SQLiteCommand sqliteCommand = command as SQLiteCommand;
            return sqliteCommand.ExecuteReader(CommandBehavior.CloseConnection);

        }

        public DataTable SelectQuery(IDbCommand command)
        {
            DataTable dataTable = new DataTable();

            using (SQLiteCommand sqliteCommand = command as SQLiteCommand)
            {
                SQLiteDataReader reader = sqliteCommand.ExecuteReader();
                dataTable.Load(reader);
            }

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        ~SqliteDataProvider()
        {
            this.Dispose();
        }
  
    }
}
