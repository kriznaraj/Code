using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Controls.Logging
{
    [Serializable]
    [XmlRoot("LoggerConfiguration")]
    public class LoggerConfiguration : ILoggerConfiguration, ISerializable
    {
        /// <summary>
        /// The logger
        /// </summary>
        private Log logger;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        ///
        [XmlElement("Logger")]
        public Log Logger
        {
            get
            {
                return this.logger;
            }
            set
            {
                this.logger = value;
            }
        }

        /// <summary>
        /// The formatter
        /// </summary>
        ///
        private Formatter formatter;

        /// <summary>
        /// Gets or sets the formatter.
        /// </summary>
        /// <value>The formatter.</value>
        ///
        [XmlElement("Formatter")]
        public Formatter Formatter
        {
            get
            {
                return this.formatter;
            }
            set
            {
                this.formatter = value;
            }
        }

        /// <summary>
        /// Default field formatter
        /// </summary>
        private string defaultFieldFormatter;

        /// <summary>
        /// Default type formatter
        /// </summary>
        private string defaultTypeFormatter;

        /// <summary>
        /// Returns field collection
        /// </summary>
        private Dictionary<string, string> fieldCollection = null;

        /// <summary>
        /// Returns field collection
        /// </summary>
        private Dictionary<string, LogLevel> logLevelCollection = null;

        /// <summary>
        /// Return logger collection
        /// </summary>
        private Dictionary<string, string> loggerPropertyCollection = null;

        /// <summary>
        /// Type of the logger
        /// </summary>
        private string type;

        /// <summary>
        /// Returns type collection
        /// </summary>
        private Dictionary<string, string> typeCollection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerConfiguration"/> class.
        /// </summary>
        public LoggerConfiguration()
        {
            this.loggerPropertyCollection = new Dictionary<string, string>();
            this.typeCollection = new Dictionary<string, string>();
            this.fieldCollection = new Dictionary<string, string>();
            this.logLevelCollection = new Dictionary<string, LogLevel>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerConfiguration"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="formatter">The formatter.</param>
        public LoggerConfiguration(Log logger, Formatter formatter)
        {
            this.logger = logger;
            this.formatter = formatter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerConfiguration"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public LoggerConfiguration(SerializationInfo info, StreamingContext context)
        {
            this.logger = (Log)info.GetValue("Log", typeof(Log));
            this.formatter = (Formatter)info.GetValue("Formatter", typeof(Formatter));
            this.Fill();
        }

        /// <summary>
        /// Fills this instance.
        /// </summary>
        public void Fill()
        {
            this.type = this.Logger.Type;
            this.defaultTypeFormatter = this.Formatter.DefaultType;
            this.defaultFieldFormatter = this.Formatter.DefaultField;

            foreach (var item in this.Logger.FormatterCollection)
            {
                this.loggerPropertyCollection[item.Key] = item.Value;
            }

            foreach (var type in this.Formatter.Types)
            {
                this.typeCollection[type.Key] = type.Value;
            }

            foreach (var field in this.Formatter.Fields)
            {
                this.fieldCollection[field.Key] = field.Value;
            }

            foreach (var item in this.logger.LogLevelCollection)
            {
                this.logLevelCollection.Add(item.ComponentName, item.LogLevel);
            }
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Logger", Logger);
            info.AddValue("Formatter", Formatter);
        }

        /// <summary>
        /// Returns default field formatter
        /// </summary>
        /// <returns></returns>
        public string GetDefaultFieldFormatter()
        {
            return this.defaultFieldFormatter;
        }

        /// <summary>
        /// Returns default type formatter
        /// </summary>
        /// <returns></returns>
        public string GetDefaultTypeFormatter()
        {
            return this.defaultTypeFormatter;
        }

        /// <summary>
        /// Returns field configuration collection
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetFieldFormatterCollection()
        {
            return this.fieldCollection;
        }

        /// <summary>
        /// Returns logger configuration collection
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetLoggerPropertyCollection()
        {
            return this.loggerPropertyCollection;
        }

        /// <summary>
        /// Returns type of logger
        /// </summary>
        /// <returns></returns>
        public string GetLoggerType()
        {
            return this.type;
        }

        /// <summary>
        /// Returns type configuration collection
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetTypeFormatterCollection()
        {
            return this.typeCollection;
        }

        /// <summary>
        /// Provides an option to override the log level at each component level
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, LogLevel> GetLoggerLevelCollection()
        {
            return this.logLevelCollection;
        }
    }

    [Serializable]
    public class Log : ISerializable
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets the formatter collection.
        /// </summary>
        /// <value>The formatter collection.</value>
        ///
        [XmlElement("Add")]
        public List<KeyValue> FormatterCollection { get; set; }

        [XmlArray("LogLevelCollection")]
        public List<LogLevelConfig> LogLevelCollection { get; set; }

        public Log()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="formatterCollection">The formatter collection.</param>
        public Log(string type, List<KeyValue> formatterCollection, List<LogLevelConfig> logLevelCollection)
        {
            this.Type = type;
            this.FormatterCollection = formatterCollection;
            this.LogLevelCollection = logLevelCollection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public Log(SerializationInfo info, StreamingContext context)
        {
            this.Type = info.GetString("Type");
            this.FormatterCollection = (List<KeyValue>)info.GetValue("FormatterCollection", typeof(List<KeyValue>));
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Type", Type);
            info.AddValue("FormatterCollection", FormatterCollection);
        }
    }

    [Serializable]
    public class Formatter : ISerializable
    {
        /// <summary>
        /// Gets the default type.
        /// </summary>
        /// <value>The default type.</value>
        public string DefaultType { get; set; }

        /// <summary>
        /// Gets the default field.
        /// </summary>
        /// <value>The default field.</value>
        public string DefaultField { get; set; }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <value>The types.</value>
        ///
        [XmlElement("Types")]
        public List<KeyValue> Types { get; set; }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        /////
        [XmlElement("Fields")]
        public List<KeyValue> Fields { get; set; }

        public Formatter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Formatter"/> class.
        /// </summary>
        /// <param name="defaultType">The default type.</param>
        /// <param name="defaultField">The default field.</param>
        /// <param name="types">The types.</param>
        /// <param name="fields">The fields.</param>
        public Formatter(string defaultType, string defaultField, List<KeyValue> types, List<KeyValue> fields)
        {
            this.DefaultType = defaultType;
            this.DefaultField = defaultField;
            this.Types = types;
            this.Fields = fields;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Formatter"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public Formatter(SerializationInfo info, StreamingContext context)
        {
            this.DefaultType = info.GetString("DefaultType");
            this.DefaultField = info.GetString("DefaultField");
            this.Types = (List<KeyValue>)info.GetValue("Types", typeof(KeyValue));
            this.Fields = (List<KeyValue>)info.GetValue("Fields", typeof(KeyValue));
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DefaultType", this.DefaultType);
            info.AddValue("DefaultField", this.DefaultField);
            info.AddValue("Types", this.Types);
            info.AddValue("Fields", this.Fields);
        }
    }

    [Serializable]
    public class KeyValue
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public KeyValue()
        {
        }

        public KeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    [Serializable]
    [XmlType("LogLevelConfig")]
    public class LogLevelConfig
    {
        [XmlAttribute("name")]
        public string ComponentName { get; set; }

        [XmlAttribute("logLevel")]
        public LogLevel LogLevel { get; set; }
    }
}