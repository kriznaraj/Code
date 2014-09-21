using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Controls.Debugging
{
    [Serializable]
    public class FileName : ISerializable
    {
        public FileName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileName"/> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="maxFileSize">Maximum size of the file.</param>
        /// <param name="traceNamePosition">The trace name position.</param>
        public FileName(string format, int maxFileSize, int traceNamePosition)
        {
            this.Format = format;
            this.MaxFileSize = maxFileSize;
            this.TraceNamePosition = traceNamePosition;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileName"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public FileName(SerializationInfo info, StreamingContext context)
        {
            this.Format = info.GetString("Format");
            this.MaxFileSize = info.GetInt32("MaxFileSize");
            this.TraceNamePosition = info.GetInt32("TraceNamePosition");
        }

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>The format.</value>
        [XmlAttribute]
        public string Format { get; set; }

        /// <summary>
        /// Gets the maximum size of the file.
        /// </summary>
        /// <value>The maximum size of the file.</value>
        [XmlAttribute]
        public int MaxFileSize { get; set; }

        /// <summary>
        /// Gets the trace name position.
        /// </summary>
        /// <value>The trace name position.</value>
        [XmlAttribute]
        public int TraceNamePosition { get; set; }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Format", this.Format);
            info.AddValue("MaxFileSize", this.MaxFileSize);
            info.AddValue("TraceNamePosition", this.TraceNamePosition);
        }
    }

    [Serializable]
    [XmlRoot("Handler")]
    public class Handler : ISerializable
    {
        public Handler()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="traceOptions">The trace options.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="fileName">Name of the file.</param>
        public Handler(string type, int traceOptions, int filter, FileName fileName)
        {
            this.Type = type;
            this.TraceOptions = traceOptions;
            this.Filter = filter;
            this.FileName = fileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public Handler(SerializationInfo info, StreamingContext context)
        {
            this.Type = info.GetString("Type");
            this.TraceOptions = info.GetInt32("TraceOptions");
            this.Filter = info.GetInt32("Filter");
            this.FileName = (FileName)info.GetValue("FileName", typeof(FileName));
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        ///
        [XmlElement("FileName")]
        public FileName FileName { get; set; }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        [XmlAttribute]
        public int Filter { get; set; }

        /// <summary>
        /// Gets the trace options.
        /// </summary>
        /// <value>The trace options.</value>
        [XmlAttribute]
        public int TraceOptions { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute]
        public string Type { get; set; }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Type", this.Type);
            info.AddValue("TraceOptions", this.TraceOptions);
            info.AddValue("Filter", this.Filter);
            info.AddValue("FileName", this.FileName);
        }
    }

    [Serializable]
    public class Trace : ISerializable
    {
        public Trace()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trace"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="sourceLevel">The source level.</param>
        /// <param name="handlers">The handlers.</param>
        public Trace(string id, string name, int sourceLevel, List<Handler> handlers)
        {
            this.Id = id;
            this.Name = name;
            this.SourceLevel = sourceLevel;
            this.Handlers = handlers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trace"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public Trace(SerializationInfo info, StreamingContext context)
        {
            this.Id = info.GetString("Id");
            this.Name = info.GetString("Name");
            this.SourceLevel = info.GetInt32("SourceLevel");
            this.Handlers = (List<Handler>)info.GetValue("Handlers", typeof(List<Handler>));
        }

        /// <summary>
        /// Gets the handlers.
        /// </summary>
        /// <value>The handlers.</value>
        ///
        [XmlElement("Handler")]
        public List<Handler> Handlers { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [XmlAttribute("ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets the source level.
        /// </summary>
        /// <value>The source level.</value>
        [XmlAttribute]
        public int SourceLevel { get; set; }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", this.Id);
            info.AddValue("Name", this.Name);
            info.AddValue("SourceLevel", this.SourceLevel);
            info.AddValue("Handlers", this.Handlers);
        }
    }

    [Serializable]
    [XmlRoot("TraceConfiguration")]
    public class TraceConfiguration : ITraceConfiguration, ISerializable
    {
        /// <summary>
        /// Key value pair containing trace writer against a unique id
        /// </summary>
        private Dictionary<string, ITraceWriter> traceCollection;

        /// <summary>
        /// The traces
        /// </summary>
        private List<Trace> traces;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceConfiguration"/> class.
        /// </summary>
        public TraceConfiguration()
        {
            this.traceCollection = new Dictionary<string, ITraceWriter>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceConfiguration"/> class.
        /// </summary>
        /// <param name="isDirectoryPerTrace">if set to <c>true</c> [is directory per trace].</param>
        /// <param name="traces">The traces.</param>
        public TraceConfiguration(bool isDirectoryPerTrace, List<Trace> traces)
            : this()
        {
            this.IsDirectoryPerTrace = isDirectoryPerTrace;
            this.traces = traces;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceConfiguration"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="contetxt">The contetxt.</param>
        public TraceConfiguration(SerializationInfo info, StreamingContext contetxt)
            : this()
        {
            this.IsDirectoryPerTrace = info.GetBoolean("isDirectoryPerTrace");
            this.traces = (List<Trace>)info.GetValue("traces", typeof(List<Trace>));
        }

        /// <summary>
        /// The is directory per trace
        /// </summary>
        public bool IsDirectoryPerTrace { get; set; }

        /// <summary>
        /// The traces
        /// </summary>
        ///
        [XmlElement("Trace")]
        public List<Trace> Traces
        {
            get
            {
                return this.traces;
            }
            set
            {
                this.traces = value;
            }
        }

        /// <summary>
        /// To get the default trace writer
        /// </summary>
        /// <returns>Default trace writer</returns>
        public ITraceWriter GetDefaultTrace()
        {
            return new TraceWriter(string.Empty, new NullTraceHandler());
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("isDirectoryPerTrace", this.IsDirectoryPerTrace);
            info.AddValue("traces", this.traces);
        }

        /// <summary>
        /// To get the list of trace writers
        /// </summary>
        /// <returns>Key value pair consisting of Id and trace writer</returns>
        public IDictionary<string, ITraceWriter> GetTraceCollection()
        {
            return this.traceCollection;
        }

        /// <summary>
        /// Reads the trace configuration from the configuration file
        /// </summary>
        public void Fill()
        {
            if (this.traceCollection != null && this.traceCollection.Count == 0)
            {
                foreach (var trace in this.traces)
                {
                    SourceLevels sourceLevel = (SourceLevels)trace.SourceLevel;

                    this.traceCollection.Add(trace.Id, GetTraceWriter(trace.Id, trace.Name, sourceLevel, trace));
                }
            }
        }

        /// <summary>
        /// Gets the trace file format object
        /// </summary>
        /// <param name="traceName">Name of the trace</param>
        /// <param name="handler">Xmlnode containg information about the trace handler</param>
        /// <returns>File format object that defines the trace file name and the maximum file size</returns>
        private ITraceFileFormat GetTraceFileFormat(string traceName, Handler handler)
        {
            FileName fileFormatNode = handler.FileName;

            return new TraceFileFormat(this.IsDirectoryPerTrace,
                                        fileFormatNode.Format,
                                        traceName,
                                        fileFormatNode.MaxFileSize,
                                        fileFormatNode.TraceNamePosition);
        }

        /// <summary>
        /// Gets the trace writer component
        /// </summary>
        /// <param name="id">Unique identifier for a Trace</param>
        /// <param name="traceName">Name of the trace</param>
        /// <param name="sourceLevels">Level of the trace message</param>
        /// <param name="trace">Xmlnode containing information about the trace</param>
        /// <returns>Trace writer object</returns>
        private ITraceWriter GetTraceWriter(string id, string traceName, SourceLevels sourceLevels, Trace trace)
        {
            TraceSource traceSource = new TraceSource(traceName) { Switch = new SourceSwitch(traceName) { Level = sourceLevels } };
            IList<Writer> traceHandlerStreamWriter = new List<Writer>();

            foreach (Handler handler in trace.Handlers)
            {
                traceHandlerStreamWriter.Add(this.GetWriter(traceName, traceSource, sourceLevels, handler));
            }

            return new TraceWriter(id, new TraceHandler(traceSource, traceHandlerStreamWriter));
        }

        /// <summary>
        /// Returns a stream Writer each listener associated with the trace source
        /// </summary>
        /// <param name="traceName">Name of the trace</param>
        /// <param name="traceSource">Trace source to which listeners can be attached</param>
        /// <param name="sourceLevels">Level of the trace message</param>
        /// <param name="handler">Xmlnode containing information about the trace handler</param>
        /// <returns></returns>
        private Writer GetWriter(string traceName, TraceSource traceSource, SourceLevels sourceLevels, Handler handler)
        {
            Type type = Type.GetType(handler.Type, true);
            TraceOptions traceOptions = (TraceOptions)handler.TraceOptions;
            SourceLevels filterLevels = (SourceLevels)handler.Filter;

            return Activator.CreateInstance(type, traceSource, filterLevels, traceOptions, this.GetTraceFileFormat(traceName, handler)) as Writer;
        }

        private class NullTraceHandler : ITraceHandler
        {
            public void Handle<T>(ITrace<T> data) where T : ISerializable
            {
            }

            public void Dispose()
            {
            }
        }
    }
}