using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Controls.Debugging
{
    /// <summary>
    /// Represents position of the trace name in trace file name
    /// </summary>
    internal enum TraceNamePosition
    {
        Prefix = 1,
        Postfix = 2,
        None = 3
    }

    /// <summary>
    /// Provides properties and methods for naming the trace file and to get the file path to which the trace         file has to be written
    /// </summary>
    internal class TraceFileFormat : ITraceFileFormat
    {
        /// <summary>
        /// Collection which holds tracer configuration
        /// </summary>
        private IDictionary<string, string> dateTimeKeyValuePair;

        /// <summary>
        /// The name of the trace
        /// </summary>
        private string traceName;

        /// <summary>
        /// True if new directory has to be created for each trace name
        /// </summary>
        private bool isDirectoryPerTrace;

        /// <summary>
        /// Maximum size of the trace file
        /// </summary>
        private int maxFileSize;

        /// <summary>
        /// The format of the trace file name
        /// </summary>
        private string fileNameFormat;

        /// <summary>
        /// Array containing the order of the trace file format
        /// </summary>
        private string[] formatArray;

        /// <summary>
        /// The format of the trace file name
        /// </summary>
        private string fileName;

        /// <summary>
        /// Returns position of the trace name in trace file name
        /// </summary>
        private TraceNamePosition traceNamePosition;

        /// <summary>
        /// Number of trace files in a given hour
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Returns Base directory path with trace folder name
        /// </summary>
        private string TracePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Trace");
            }
        }

        /// <summary>
        /// Returns the maximum size of trace file
        /// </summary>
        /// <returns>maximum size of trace file in bytes</returns>
        public int GetMaxSize()
        {
            return this.maxFileSize * 1024;
        }

        /// <summary>
        /// Intializes the trace file format component
        /// </summary>
        /// <param name="isDirectoryPerTrace">True if new directory has to be created for each trace name                        </param>
        /// <param name="fileFormat">The format of the trace file name</param>
        /// <param name="traceName">The name of the trace</param>
        /// <param name="maxFileSize">Maximum size of the trace file</param>
        /// <param name="tracePostion">Position of the trace name in trace file name</param>
        public TraceFileFormat(bool isDirectoryPerTrace, string fileFormat, string traceName, int maxFileSize, int tracePostion)
        {
            this.dateTimeKeyValuePair = new Dictionary<string, string>();
            this.isDirectoryPerTrace = isDirectoryPerTrace;
            this.fileNameFormat = fileFormat;
            this.maxFileSize = maxFileSize;
            this.traceName = traceName;
            this.traceNamePosition = (TraceNamePosition)tracePostion;
            this.formatArray = this.fileNameFormat.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns the path of the trace file with full file name
        /// </summary>
        /// <param name="extension">Extension of the trace file</param>
        /// <returns>The path where the trace file has to be written</returns>
        private string GetTraceFilePath(string extension)
        {
            if (!this.isDirectoryPerTrace)
            {
                return Path.Combine(this.TracePath, this.fileName + this.counter + "." + extension);
            }
            else
            {
                return Path.Combine(this.TracePath, this.traceName, this.fileName + this.counter + "." + extension);
            }
        }

        /// <summary>
        /// Creates the directory to which the trace file has to be written
        /// </summary>
        /// <param name="traceName">Name of the trace</param>
        /// <returns>The trace file path</returns>
        private String CreateDirectoryStructure(string traceName)
        {
            String DirectoryPath = (true == this.isDirectoryPerTrace) ? Path.Combine(this.TracePath, traceName) : this.TracePath;

            if (false == Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            return DirectoryPath;
        }

        /// <summary>
        /// To get the name of the trace file
        /// </summary>
        /// <returns>Trace file name</returns>
        private string GetFileName()
        {
            StringBuilder fileName = new StringBuilder();

            foreach (string format in this.formatArray)
            {
                switch (format)
                {
                    case "dd":
                    case "ddd":
                    case "MM":
                    case "MMM":
                    case "yyyy":
                    case "yy":
                    case "HH":
                    case "hh":

                        fileName.Append(DateTimeOffset.UtcNow.ToString(format));
                        this.dateTimeKeyValuePair[format] = DateTimeOffset.UtcNow.ToString(format);
                        break;

                    case "Source":
                        break;

                    default:
                        fileName.Append(format);
                        break;
                }
            }

            this.RemoveInvalidFileNameChars(fileName);
            this.AddComponentName(this.traceName, fileName);
            return fileName.ToString();
        }

        /// <summary>
        /// To remove invalid chars in the file name.
        /// </summary>
        /// <param name="fileName">Name of the Trace file</param>
        private void RemoveInvalidFileNameChars(StringBuilder fileName)
        {
            foreach (char invalidChars in Path.GetInvalidFileNameChars())
            {
                if (fileName.ToString().Contains(invalidChars))
                {
                    fileName.Replace(invalidChars.ToString(), "");
                }
            }
        }

        /// <summary>
        /// Sets the counter that keeps track of the number of trace files generated for the hour
        /// </summary>
        private void SetCounter()
        {
            foreach (string format in this.dateTimeKeyValuePair.Keys)
            {
                if (DateTimeOffset.UtcNow.ToString(format) != this.dateTimeKeyValuePair[format])
                {
                    this.counter = 0;
                    return;
                }
            }
        }

        /// <summary>
        /// To append or prepend name of the trace to trace file name
        /// </summary>
        /// <param name="traceName">Trace name</param>
        /// <param name="fileName">Trace file name</param>
        private void AddComponentName(string traceName, StringBuilder fileName)
        {
            switch (this.traceNamePosition)
            {
                case TraceNamePosition.Prefix:
                    fileName.Insert(0, traceName + "_");
                    break;

                case TraceNamePosition.Postfix:
                    fileName.Append("_" + traceName);
                    break;
            }
            fileName.Append("_");
        }

        /// <summary>
        /// Gets the file path to which the trace file has to be written
        /// </summary>
        /// <param name="extension">Trace file extension</param>
        /// <returns>file path to which the trace file has to be written</returns>
        public string GetFilePath(string extension)
        {
            string path = this.CreateDirectoryStructure(this.traceName);
            SetCounter();
            this.fileName = GetFileName();
            this.counter = Directory.GetFiles(path, fileName + "*." + extension).Length + 1;
            return this.GetTraceFilePath(extension);
        }
    }
}