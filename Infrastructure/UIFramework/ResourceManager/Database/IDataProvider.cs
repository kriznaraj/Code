using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.UI.Web.ResourceManager
{
    public interface IDataProvider : IDisposable
    {
        /// <summary>
        /// This Property Contains insert query text.
        /// </summary>
        string InsertQuery { get; }

        /// <summary>
        /// This Property Contains update query text.
        /// </summary>
        string UpdateQuery { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        DataTable SelectQuery(IDbCommand command);

        DbParameter[] GetParameter(List<KeyValuePair<string, object>> parameterList);

        /// <summary>
        /// Property retuns database connection object.
        /// </summary>
        IDbConnection GetConnection { get; }

        /// <summary>
        /// Returns a command object for database execution.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dataParameter"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandType"></param>
        /// <returns>Command object</returns>
        IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, IDbTransaction dbTransaction, CommandType commandType = CommandType.Text);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dataParameter"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, CommandType commandType = CommandType.Text);

        /// <summary>
        /// Return a database transaction
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns>Transaction object</returns>
        IDbTransaction GetTransaction(IDbConnection dbConnection);

        /// <summary>
        /// Returns true if Executed successfully otheriwse false.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>bool</returns>
        bool ExecuteNonQuery(IDbCommand command);

        /// <summary>
        /// Return a DataTable if Executed successfully.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>DataTable</returns>
        DbDataReader ExecuteQuery(IDbCommand command);

    }
}
