using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Types
{
    /// <summary>
    /// Exception thrown by the framework on receiving Optimistic Lock Violation form SQL Server corresponding to Error Number 999999
    /// </summary>
    public sealed class OptimisticLockException : Exception
    {
        /// <summary>
        /// Creates a new instance of the Optimistic Lock Exception with the Error Number, Sql Exception
        /// </summary>
        /// <param name="errorNumber">Error Number</param>
        /// <param name="sqlException">Sql Exception</param>
        public OptimisticLockException(long errorNumber, SqlException sqlException)
        {
            this.ErrorNumber = errorNumber;
            this.SqlException = sqlException;
        }

        /// <summary>
        /// Gets or Sets the error number associated with the Sql Exception
        /// </summary>
        public long ErrorNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the SqlException associated with the object
        /// </summary>
        public SqlException SqlException
        {
            get;
            private set;
        }
    }
}