using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Controls.Debugging
{
    [Serializable]
    [XmlRoot("PerformanceCounter")]
    public class InstrumentationConfiguration : IInstrumentationConfiguration, ISerializable
    {
        /// <summary>
        /// The categories
        /// </summary>
        private List<Category> categories;

        /// <summary>
        /// The _categories
        /// </summary>
        ///
        [XmlElement("Category")]
        public List<Category> Categories
        {
            get
            {
                return this.categories;
            }
            set
            {
                this.categories = value;
            }
        }

        /// <summary>
        /// Key value pair containing id and performance counter
        /// </summary>
        private Dictionary<string, ICounter> counterCollection;

        /// <summary>
        /// Gets the name of the instance.
        /// </summary>
        /// <value>The name of the instance.</value>
        [XmlAttribute]
        public string InstanceName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentationConfiguration"/> class.
        /// </summary>
        public InstrumentationConfiguration()
        {
            this.counterCollection = new Dictionary<string, ICounter>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentationConfiguration"/> class.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="catergories">The catergories.</param>
        public InstrumentationConfiguration(string instanceName, List<Category> catergories)
            : this()
        {
            this.InstanceName = instanceName;
            this.categories = catergories;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentationConfiguration"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public InstrumentationConfiguration(SerializationInfo info, StreamingContext context)
            : this()
        {
            this.InstanceName = info.GetString("instanceName");
            this.categories = (List<Category>)info.GetValue("categories", typeof(List<Category>));
        }

        /// <summary>
        /// Fills this instance.
        /// </summary>
        public void Fill()
        {
            foreach (var category in categories)
            {
                string categoryName = category.Name;
                string description = category.Description;

                this.RegisterCounterCategory(categoryName, description, this.GetCounters(categoryName, description, category));
            }
        }

        /// <summary>
        /// Registers the counter category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="description">The description.</param>
        /// <param name="counterCreationDataCollection">The counter creation data collection.</param>
        private void RegisterCounterCategory(string categoryName, string description, CounterCreationDataCollection counterCreationDataCollection)
        {
            if (counterCreationDataCollection.Count > 0)
            {
                if (false == PerformanceCounterCategory.Exists(categoryName))
                {
                    PerformanceCounterCategory.Create(categoryName, description, PerformanceCounterCategoryType.MultiInstance, counterCreationDataCollection);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryName">Name of the custom performance category</param>
        /// <param name="description">Description for the custom performance category</param>
        /// <param name="category">category object</param>
        /// <returns></returns>
        private CounterCreationDataCollection GetCounters(string categoryName, string description, Category category)
        {
            CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection();

            foreach (var counter in category.Counters)
            {
                int id = counter.Id;
                string counterName = counter.Name;

                PerformanceCounterType performanceCounterType;
                if (false == Enum.TryParse<PerformanceCounterType>(counter.Type, out performanceCounterType))
                {
                    throw new ArgumentException(string.Format("Requested value {0} was not found.", counter.Type));
                }

                counterCreationDataCollection.Add(new CounterCreationData(counterName, description, performanceCounterType));
                counterCollection.Add(counterName, new Counter(id, categoryName, counterName, this.InstanceName));
            }

            return counterCreationDataCollection;
        }

        /// <summary>
        /// To get the list of performance counters
        /// </summary>
        /// <returns>Key value pair consisting of Id and Performance Counter</returns>
        public IDictionary<string, ICounter> GetCounterCollection()
        {
            return this.counterCollection;
        }

        /// <summary>
        /// To get the default performance counter
        /// </summary>
        /// <returns>Default performance counter</returns>
        public ICounter GetDefaultCounter()
        {
            return new NullCounter();
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("instanceName", this.InstanceName);
            info.AddValue("categories", this.categories);
        }

        private class NullCounter : ICounter
        {
            public int ID
            {
                get { return Int32.MinValue; }
            }

            public void Increment()
            {
            }

            public void Decrement()
            {
            }

            public void IncrementBy(long valueToIncrement)
            {
            }

            public void Reset()
            {
            }

            public void Dispose()
            {
            }
        }
    }

    [Serializable]
    [XmlRoot("Category")]
    public class Category : ISerializable
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlAttribute]
        public string Description { get; set; }

        /// <summary>
        /// Gets the counters.
        /// </summary>
        /// <value>The counters.</value>
        ///
        [XmlElement("Counters")]
        public List<Counters> Counters { get; set; }

        public Category()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="counters">The counters.</param>
        public Category(string name, string description, List<Counters> counters)
        {
            this.Name = name;
            this.Description = description;
            this.Counters = counters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public Category(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("Name");
            this.Description = info.GetString("Description");
            this.Counters = (List<Counters>)info.GetValue("Counters", typeof(List<Counters>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("Description", this.Description);
            info.AddValue("Counters", this.Counters);
        }
    }

    [Serializable]
    [XmlRoot("Counters")]
    public class Counters : ISerializable
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [XmlAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute]
        public string Type { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlAttribute]
        public string Description { get; set; }

        public Counters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Counters"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="description">The description.</param>
        public Counters(int id, string name, string type, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Counters"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public Counters(SerializationInfo info, StreamingContext context)
        {
            this.Id = info.GetInt32("Id");
            this.Name = info.GetString("Name");
            this.Type = info.GetString("Type");
            this.Description = info.GetString("Description");
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", this.Id);
            info.AddValue("Name", this.Name);
            info.AddValue("Type", this.Type);
            info.AddValue("Description", this.Description);
        }
    }
}