using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Types
{
    /// <summary>
    /// Exception thrown by the framework on receiving SQL Exception with Error Number 2627
    /// </summary>
    public sealed class DuplicateKeyException : Exception
    {
        /// <summary>
        /// Creates a new instance of the Duplicate Key Exception with the Error Number, Sql Exception
        /// </summary>
        /// <param name="errorNumber">Error Number</param>
        /// <param name="sqlException">Sql Exception</param>
        public DuplicateKeyException(long errorNumber, SqlException sqlException)
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
            set;
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