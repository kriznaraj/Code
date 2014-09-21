using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.SqlServerCe;


namespace BallyTech.UI.Web.ResourceManager
{
    public class SqlCEDataProvider : IDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private string connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlCEDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbParameter[] GetParameter(List<KeyValuePair<string, object>> parameterList)
        {
            List<SqlCeParameter> parameters = new List<SqlCeParameter>(parameterList.Count);
            parameterList.ForEach(o => parameters.Add(new SqlCeParameter(o.Key, o.Value)));
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
            get { return new SqlCeConnection (this.connectionString); }
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
            SqlCeCommand sqlCeCommand = dbConnection.CreateCommand() as SqlCeCommand;
            sqlCeCommand.CommandText = commandText;
            sqlCeCommand.CommandType = commandType;
            sqlCeCommand.Connection = dbConnection as SqlCeConnection;
            sqlCeCommand.Parameters.AddRange(dataParameter);

            return sqlCeCommand;
        }

        public IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, CommandType commandType = CommandType.Text)
        {
            SqlCeCommand sqlCeCommand = dbConnection.CreateCommand() as SqlCeCommand;
            sqlCeCommand.CommandText = commandText;
            sqlCeCommand.CommandType = commandType;
            sqlCeCommand.Connection = dbConnection as SqlCeConnection;
            if (dataParameter != null)
            {
                sqlCeCommand.Parameters.AddRange(dataParameter);
            }

            return sqlCeCommand;
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

            SqlCeCommand sqliteCommand = command as SqlCeCommand;
            return sqliteCommand.ExecuteReader(CommandBehavior.CloseConnection);

        }

        public DataTable SelectQuery(IDbCommand command)
        {
            DataTable dataTable = new DataTable();

            using (SqlCeCommand sqlCeCommand = command as SqlCeCommand)
            {
                SqlCeDataReader reader = sqlCeCommand.ExecuteReader();
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

        ~SqlCEDataProvider()
        {
            this.Dispose();
        }

    }
}
