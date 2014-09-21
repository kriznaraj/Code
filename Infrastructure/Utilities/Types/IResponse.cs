using System.Collections;

namespace Controls.Types
{
    /// <summary>
    /// Interface to be implemented by the response object with no result
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Result of the current call
        /// </summary>
        ResultType Result { get; }

        /// <summary>
        /// Parameter collection to be used for building error object
        /// </summary>
        IDictionary Params { get; }

        /// <summary>
        /// Response Error code
        /// </summary>
        long[] ErrorCodes { get; }
    }

    /// <summary>
    /// Interface to be implemented by the response object with result
    /// </summary>
    /// <typeparam name="T">Type of the Result object</typeparam>
    public interface IResponse<T> : IResponse
    {
        /// <summary>
        /// Response Data
        /// </summary>
        T Data { get; }
    }
}