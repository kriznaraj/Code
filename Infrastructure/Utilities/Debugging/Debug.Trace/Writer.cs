using System.Diagnostics;
using System.IO;
using Controls.Types;

namespace Controls.Debugging
{
    /// <summary>
    /// Abstract class that provides set of methods to handle the stream writer required for each trace handler
    /// </summary>
    public abstract class Writer : CriticalFinalizer
    {
        /// <summary>
        /// File format component for each handler
        /// </summary>
        private ITraceFileFormat traceFileFormat;

        /// <summary>
        /// Extension of trace file name for each handler
        /// </summary>
        private string fileExtension;

        /// <summary>
        /// To write data for each handler
        /// </summary>
        private StreamWriter streamWriter;

        /// <summary>
        /// Trace source that holds a set of handlers
        /// </summary>
        private TraceSource traceSource;

        /// <summary>
        /// The level of trace message filtered by trace listener
        /// </summary>
        private SourceLevels filterLevels;

        /// <summary>
        /// Trace data options that has to be written in the trace output
        /// </summary>
        private TraceOptions traceOptions;

        /// <summary>
        /// Trace listener object associated with the trace source
        /// </summary>
        private TraceListener traceListener;

        /// <summary>
        /// Initializes the traceHandlerStreamWriter object
        /// </summary>
        /// <param name="traceSource">Trace source that holds a set of handlers</param>
        /// <param name="filterLevels">The level of trace message filtered by trace listener</param>
        /// <param name="traceOptions">Trace data options that has to be written in the trace output</param>
        /// <param name="traceFileFormat">File format component for the handler</param>
        /// <param name="fileExtension">Extension of trace file name for each handler</param>
        public Writer(TraceSource traceSource, SourceLevels filterLevels, TraceOptions traceOptions, ITraceFileFormat traceFileFormat, string fileExtension = "txt")
        {
            this.traceSource = traceSource;
            this.filterLevels = filterLevels;
            this.traceOptions = traceOptions;
            this.traceFileFormat = traceFileFormat;
            this.fileExtension = fileExtension;
        }

        /// <summary>
        /// Abstract method to initialize the trace listeners associated with the trace source
        /// </summary>
        /// <param name="traceSource">Trace source that holds a set of handlers</param>
        /// <param name="filterLevels">The level of trace message filtered by trace listener</param>
        /// <param name="traceOptions">Trace data options that has to be written in the trace output</param>
        /// <param name="traceListener">Trace listener object associated with the trace source</param>
        public abstract void InitListener(out TraceListener traceListener);

        /// <summary>
        /// Handles stream writer for each trace handler
        /// </summary>
        public void HandleStreamWriter()
        {
            if (this.streamWriter != null && this.streamWriter.BaseStream.Length > this.traceFileFormat.GetMaxSize())
            {
                this.streamWriter.Flush();
                this.streamWriter.Dispose();
                TextWriterTraceListener textWriterTraceListener = this.traceListener as TextWriterTraceListener;
                if (textWriterTraceListener != null)
                {
                    textWriterTraceListener.Writer = this.GetStreamWriter();
                }
            }
            else if (this.traceListener == null)
            {
                this.InitListener(out this.traceListener);
                if (this.traceListener != null)
                {
                    this.traceListener.Filter = new EventTypeFilter(filterLevels);
                    this.traceListener.TraceOutputOptions = traceOptions;
                    this.traceSource.Listeners.Add(this.traceListener);
                }
            }
        }

        /// <summary>
        /// Gets the stream writer to write trace messages
        /// </summary>
        /// <returns>Stream writer object</returns>
        public StreamWriter GetStreamWriter()
        {
            //this.Register(this.streamWriter, ResourceType.Unmanaged);
            return this.streamWriter = new StreamWriter(this.traceFileFormat.GetFilePath(this.fileExtension)) { AutoFlush = true };
        }
    }
}