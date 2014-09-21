using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Types
{
    /// <summary>
    /// Operation Type
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Get operation used for retrieval
        /// </summary>
        GET,

        /// <summary>
        /// Post operation used for Insert
        /// </summary>
        POST,

        /// <summary>
        /// Put Operation used for Update
        /// </summary>
        PUT,

        /// <summary>
        /// Patch operation used for partial update
        /// </summary>
        PATCH,

        /// <summary>
        /// Delete operation used for deleting
        /// </summary>
        DELETE
    }

    /// <summary>
    /// Indicates that a method is exposed as an API for access using an HTTP protocol (RESTfull API)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class OperationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the Operation Attribute
        /// </summary>
        /// <param name="operationTarget">Target class/controller for the current operation</param>
        /// <param name="operationMember">Target Member/Action for the current operation </param>
        /// <param name="operationType">Type of the operation supported by the current API</param>
        public OperationAttribute(string operationTarget, string operationMember, OperationType operationType)
        {
            this.OperationTarget = operationTarget;
            this.OperationMember = operationMember;
            this.OperationType = operationType;
        }

        /// <summary>
        /// Initializes a new instance of the Operation Attribute
        /// </summary>
        /// <param name="operationTarget">Target class/controller for the current operation</param>
        /// <param name="operationMember">Target Member/Action for the current operation </param>
        public OperationAttribute(string operationTarget, string operationMember)
            : this(operationTarget, operationMember, OperationType.POST)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Operation Attribute
        /// </summary>
        /// <param name="operationTarget">Target class/controller for the current operation</param>
        /// <param name="operationType">Type of the operation supported by the current API</param>
        public OperationAttribute(string operationTarget, OperationType operationType)
            : this(operationTarget, string.Empty, operationType)
        {
        }

        /// <summary>
        /// Gets Or Sets the Operation Id (Similar to TranCode)
        /// </summary>
        public int OperationId { get; set; }

        /// <summary>
        /// Gets the Operation Member
        /// </summary>
        public string OperationMember { get; private set; }

        /// <summary>
        /// Gets the Operation Target
        /// </summary>
        public string OperationTarget { get; private set; }

        /// <summary>
        /// Gets the Operation Type
        /// </summary>
        public OperationType OperationType { get; private set; }

        /// <summary>
        /// Gets Or Sets the Task Id (Similar to Security Code Id)
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Checks the validity of the session
        /// </summary>
        public bool ValidateSecurity { get; set; }
    }
}