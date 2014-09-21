using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Controls.Logging
{
    /// <summary>
    /// Represents position of the component name in log file name
    /// </summary>
    internal enum ComponentPosition
    {
        /// <summary>
        /// Log file name will be prefixed with component name
        /// </summary>
        Prefix = 1,

        /// <summary>
        /// Log file name will be postfixed with component name
        /// </summary>
        Postfix = 2
    }

    /// <summary>
    ///  Template to log message
    /// </summary>
    public class FileLogging : ILogging
    {
        /// <summary>
        /// Collection which holds logger configuration
        /// </summary>
        private readonly IDictionary<string, string> dateTimeKeyValuePair;

        /// <summary>
        /// Extension for a log file
        /// </summary>
        private readonly String extension;

        /// <summary>
        /// The format of the log file name
        /// </summary>
        private readonly string fileNameFormat;

        /// <summary>
        /// Array containing the order of the log file format
        /// </summary>
        private readonly string[] formatArray;

        /// <summary>
        /// True if new directory has to be created for each component
        /// </summary>
        private readonly bool isDirectoryPerComponent;

        /// <summary>
        /// Maximum size for a log file
        /// </summary>
        private readonly Int64 maxFileSize;

        /// <summary>
        /// Array containing the order of the log message format
        /// </summary>
        private readonly string[] messageArray;

        /// <summary>
        /// The size of the current log file
        /// </summary>
        private Int64 actualFileSize = 0;

        /// <summary>
        /// Returns position of the component name in log file name
        /// </summary>
        private ComponentPosition componentPosition;

        /// <summary>
        /// Number of log files in a given hour
        /// </summary>
        private int counter = 1;

        /// <summary>
        /// Name of the current log file
        /// </summary>
        private string fileName;

        /// <summary>
        /// Stream to read or write data to the log file.
        /// </summary>
        private FileStream fileStream;

        private Dictionary<string, KeyValue<string, long>> logFileHandler;

        private const long INITIALFILESIZE = 0;

        /// <summary>
        /// Initialize logger
        /// </summary>
        /// <param name="props">Collection which has logger configuration</param>
        public FileLogging(IDictionary<string, string> props)
        {
            this.dateTimeKeyValuePair = new Dictionary<string, string>();
            this.componentPosition = (ComponentPosition)int.Parse(props["componentPositionInFileName"]);
            this.isDirectoryPerComponent = Convert.ToBoolean(props["directoryRequiredPerComponent"]);
            this.maxFileSize = Convert.ToInt64(props["filesize"]) * 1024;
            this.fileNameFormat = props["format"];
            this.messageArray = props["data"].Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            this.LogLevel = (LogLevel)int.Parse(props["logLevel"]);
            this.extension = props["fileExtension"];
            this.formatArray = fileNameFormat.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            this.logFileHandler = new Dictionary<string, KeyValue<string, long>>();
        }

        /// <summary>
        /// Returns Base directory path with log folder name
        /// </summary>
        public string LoggerPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "\\Logger";
            }
        }

        /// <summary>
        /// Represents the log level needed to determine whether the message has to be logged.
        /// </summary>
        public LogLevel LogLevel { get; private set; }

        /// <summary>
        /// Represents encoding format to read number of bytes
        /// </summary>
        private Encoding Encoding
        {
            get
            {
                return Encoding.Unicode;
            }
        }

        /// <summary>
        /// To dispose the file handler if open.
        /// </summary>
        public void Dispose()
        {
            this.CloseFile();
        }

        /// <summary>
        ///  To log the message
        /// </summary>
        /// <param name="logEntry">Logger configuration object</param>
        public void Log(LogEntry logEntry)
        {
            this.GetFile(logEntry);
            this.WriteLogFile(logEntry);
        }

        /// <summary>
        /// To remove invalid chars in the file name.
        /// </summary>
        /// <param name="fileName">Name of the log file</param>
        private static void RemoveInvalidFileNameChars(StringBuilder fileName)
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
        /// To append or prepend component name to log file name
        /// </summary>
        /// <param name="ComponentName">Name of the component</param>
        /// <param name="fileName">Name of the log file</param>
        private void AddComponentName(string ComponentName, StringBuilder fileName)
        {
            switch (this.componentPosition)
            {
                case ComponentPosition.Prefix:
                    fileName.Insert(0, ComponentName);
                    break;

                case ComponentPosition.Postfix:
                    fileName.Append(ComponentName);
                    break;
            }
        }

        /// <summary>
        /// To check if a new log file has to be created.
        /// </summary>
        /// <param name="logEntry">Logger configuration object</param>
        /// <returns>True if a new log file has to created</returns>
        private bool CheckIfNewFileRequired(LogEntry logEntry)
        {
            bool retFlag = false;
            foreach (string format in this.dateTimeKeyValuePair.Keys)
            {
                if (logEntry.Timestamp.UtcDateTime.ToString(format) != dateTimeKeyValuePair[format])
                {
                    retFlag = true;
                }
            }

            return retFlag;
        }

        private bool CheckFileReachedMaxSize(LogEntry logEntry)
        {
            bool retFlag = false;
            KeyValue<string, long> fileObject;

            if (this.logFileHandler.TryGetValue(logEntry.Source, out fileObject))
            {
                if (fileObject.Value >= maxFileSize)
                {
                    retFlag = true;
                }
            }
            else
            {
                retFlag = true; //For Initial entry
            }

            return retFlag;
        }

        private void CloseFile()
        {
            if (fileStream != null)
            {
                fileStream.Dispose();
            }
        }

        /// <summary>
        /// To construct the way message has to be logged in the log file.
        /// </summary>
        /// <param name="logEntry">Logger configuration object</param>
        /// <returns>Byte count of the log message</returns>
        private long ConstructLogMessage(LogEntry logEntry)
        {
            Int64 dataLength = 0;

            foreach (string LogData in this.messageArray)
            {
                switch (LogData)
                {
                    case "Source":
                        dataLength += this.WriteDatatoLogFile(Environment.NewLine + "Source   : " + logEntry.Source + Environment.NewLine);
                        break;

                    case "ManagedThreadId":
                        dataLength += this.WriteDatatoLogFile("Thread Id: " + logEntry.ManagedThreadId.ToString() + Environment.NewLine);
                        break;

                    case "Timestamp":
                        dataLength += this.WriteDatatoLogFile("Time     : " + logEntry.Timestamp.ToString() + Environment.NewLine);
                        break;

                    case "LogType":
                        dataLength += this.WriteDatatoLogFile("Log Type: " + logEntry.LogType.ToString() + Environment.NewLine);
                        break;

                    case "Message":
                        dataLength += this.WriteDatatoLogFile("Error    : " + logEntry.Message + Environment.NewLine);
                        break;

                    default:
                        break;
                }
            }
            return dataLength;
        }

        private void CreateDirectoryStructure(String source)
        {
            String DirectoryPath = (true == this.isDirectoryPerComponent) ? Path.Combine(this.LoggerPath, source) : this.LoggerPath;

            if (false == Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
        }

        /// <summary>
        /// Create a new log file
        /// </summary>
        /// <param name="logFileName">Name of the current log file</param>
        private void OpenLogFile(string logFilePath)
        {
            this.actualFileSize = 0;

            this.CloseFile();

            fileStream = new FileStream(logFilePath, FileMode.Append);
        }

        /// <summary>
        /// Checks if a log new file has to be created. If yes, creates one.
        /// </summary>
        /// <param name="logEntry">Logger configuration object</param>
        private void GetFile(LogEntry logEntry)
        {
            string loggerPath = this.LoggerPath +
                                ((isDirectoryPerComponent == true) ? "\\" + logEntry.Source : "");

            if (this.fileStream == null ||
                this.CheckFileReachedMaxSize(logEntry) ||
                this.CheckIfNewFileRequired(logEntry))
            {
                if (null == this.fileStream || Directory.Exists(loggerPath) == false)
                {
                    this.CreateDirectoryStructure(logEntry.Source);
                }

                this.fileName = GetFileName(logEntry);
                string[] files = Directory.GetFiles(loggerPath, this.fileName + "*." + this.extension);
                this.counter = files.Length + 1;
                this.fileName = string.Concat(this.fileName, this.counter, ".", this.extension);
                string logFilePath = GetLogFilePath(this.fileName, logEntry.Source);

                this.logFileHandler[logEntry.Source] = new KeyValue<string, long>(this.fileName, INITIALFILESIZE);

                this.OpenLogFile(logFilePath);
            }
            else
            {
                KeyValue<string, long> fileObject;

                if (this.logFileHandler.TryGetValue(logEntry.Source, out fileObject))
                {
                    string logFilePath = GetLogFilePath(fileObject.Key, logEntry.Source);

                    if (this.fileStream.Name != logFilePath)
                    {
                        this.OpenLogFile(logFilePath);
                    }
                }
            }
        }

        /// <summary>
        /// To get the name of the log file that has to be logged.
        /// </summary>
        /// <param name="logEntry">Logger configuration object</param>
        /// <returns>Name of the Log file that has to logged</returns>
        private string GetFileName(LogEntry logEntry)
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

                        fileName.Append(logEntry.Timestamp.UtcDateTime.ToString(format));
                        this.dateTimeKeyValuePair[format] = logEntry.Timestamp.UtcDateTime.ToString(format);
                        break;

                    case "Source":
                        break;

                    default:
                        fileName.Append(format);
                        break;
                }
            }

            RemoveInvalidFileNameChars(fileName);

            AddComponentName(logEntry.Source, fileName);

            return fileName.ToString();
        }

        /// <summary>
        /// Returns the path of the log file with full file name
        /// </summary>
        /// <param name="fileName">Name of the current log file</param>
        /// <param name="component">Component against which the file has to be logged</param>
        /// <returns>The path of the log file name</returns>
        private string GetLogFilePath(string fileName, string component)
        {
            if (!this.isDirectoryPerComponent)
            {
                return Path.Combine(this.LoggerPath, fileName);
            }
            else
            {
                return Path.Combine(this.LoggerPath, component, fileName);
            }
        }

        /// <summary>
        /// Writes data to log file
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <returns>byte count of the data that was logged</returns>
        private Int64 WriteDatatoLogFile(string message)
        {
            byte[] data;
            data = this.Encoding.GetBytes(message);
            this.fileStream.Write(data, 0, data.Length);
            return data.Length;
        }

        /// <summary>
        /// Start writing the log message if the file size does not exceed the Maximum file size configured for log file.
        /// </summary>
        /// <param name="logEntry">Logger configuration object</param>
        private void WriteLogFile(LogEntry logEntry)
        {
            Int64 msgLength = 0;

            if (this.actualFileSize >= maxFileSize)
            {
                GetFile(logEntry);
            }

            msgLength = ConstructLogMessage(logEntry);

            this.fileStream.Flush();
            this.actualFileSize += msgLength;
            KeyValue<string, long> fileObject;

            if (this.logFileHandler.TryGetValue(logEntry.Source, out fileObject))
            {
                fileObject.Value += msgLength;
            }
        }
    }

    class KeyValue<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public KeyValue(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}