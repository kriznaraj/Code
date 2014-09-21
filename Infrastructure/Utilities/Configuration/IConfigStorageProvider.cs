using System;
using System.Collections.Generic;
using System.Data;

namespace Controls.Configuration
{
    /// <summary>
    /// Defines set of methods for Database operation
    /// </summary>
    public interface IConfigStorageProvider : IDisposable
    {
        /// <summary>
        /// Select stored procedure name
        /// </summary>
        string SelectConfigQueryText { get; }

        /// <summary>
        /// Select All Stored Procedure Name
        /// </summary>
        string SelectAllConfigQueryText { get; }

        /// <summary>
        /// Insert stored procedure name
        /// </summary>
        string InsertConfigCommandText { get; }

        /// <summary>
        /// Update stored procedure name
        /// </summary>
        string UpdateConfigCommandText { get; }

        /// <summary>
        /// Returns database connection object
        /// </summary>
        /// <returns>Connection object</returns>
        IDbConnection GetConnection();

        /// <summary>
        /// Returns command object, to perform operation on the database
        /// </summary>
        /// <param name="commandText">Stored procedure name</param>
        /// <param name="dbConnection">Database connection object</param>
        /// <param name="dataParameter">Parameter collection</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandType">Command Type</param>
        /// <returns>Command object</returns>
        IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, IDbTransaction dbTransaction, CommandType commandType = CommandType.Text);

        /// <summary>
        /// Returns transaction object for a connection
        /// </summary>
        /// <param name="dbConnection">Database connectio object</param>
        /// <returns>Transaction object</returns>
        IDbTransaction GetTransaction(IDbConnection dbConnection);

        /// <summary>
        /// Returns true if command executed successfully else false.
        /// </summary>
        /// <param name="command">Insert/Update Command object</param>
        /// <returns>True/False</returns>
        bool ExecuteNonQuery(IDbCommand command);

        /// <summary>
        /// Returns datatable for the configuration key
        /// </summary>
        /// <param name="command">Select Command object</param>
        /// <returns>DataTable</returns>
        IEnumerable<string> ExecuteQuery(IDbCommand command);

        /// <summary>
        /// Returns parameter
        /// </summary>
        /// <returns>Parameter</returns>
        IDataParameter GetParameter(string parameterName, object value);
    }
}