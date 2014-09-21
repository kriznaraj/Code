namespace Controls.Debugging
{
    /// <summary>
    /// Provides methods to get the maximum size of trace file and file path into which the trace file has to be written
    /// </summary>
    public interface ITraceFileFormat
    {
        /// <summary>
        /// Computes the maximum size of trace file
        /// </summary>
        /// <returns>Maximum file size in bytes</returns>
        int GetMaxSize();

        /// <summary>
        /// Gets the file path into which the trace file has to be written
        /// </summary>
        /// <param name="extension">Extension of trace file</param>
        /// <returns>File path into which the trace file has to be written</returns>
        string GetFilePath(string extension);
    }
}