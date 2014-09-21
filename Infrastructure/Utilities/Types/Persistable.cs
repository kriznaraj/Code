using System;
using System.Collections.Generic;

namespace Controls.Types
{
    /// <summary>
    /// Abstract class that has to implemented by a type that has to be persisted in a data store.
    /// </summary>
    public abstract class Persistable : IPersistable
    {
        /// <summary>
        /// Key value pair containing objects to be saved along with the aggregate type.
        /// </summary>
        private readonly Dictionary<string, dynamic> compositesIndexer = new Dictionary<string, dynamic>();

        /// <summary>
        /// Instantiates the Controls.Types.Persistable class
        /// </summary>
        public Persistable()
            : this(Types.PersistableState.Unchanged, null)
        {
        }

        /// <summary>
        /// Instantiates the Controls.Types.Persistable class
        /// </summary>
        /// <param name="state">Object state</param>
        /// <param name="version">Version number of the object</param>
        public Persistable(PersistableState state, int? version)
        {
            this.State = state;
            this.Version = version;
            this.compositesIndexer = new Dictionary<string, dynamic>();
        }

        /// <summary>
        /// Instantiates the new instance of persistable object from the existing object by copying the state and version from the existing object
        /// </summary>
        /// <param name="persistable">Persistable object to be copied from</param>
        public Persistable(Persistable persistable)
            : this(persistable.State, persistable.Version)
        {
        }

        /// <summary>
        /// Gets the object state. Example : New, modified, deleted etc
        /// </summary>
        public PersistableState State
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the version muber of the object
        /// </summary>
        public int? Version
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the persistable state of the current instance
        /// </summary>
        /// <param name="persistableState"></param>
        public void SetState(PersistableState persistableState)
        {
            this.State = persistableState;
        }

        /// <summary>
        /// Called when the object is added
        /// </summary>
        public void Added()
        {
            this.State = Types.PersistableState.New;
        }

        /// <summary>
        /// Called when the object is modified.
        /// </summary>
        public void Modified()
        {
            this.State = Types.PersistableState.Modified;
        }

        /// <summary>
        /// Called when the object is deleted.
        /// </summary>
        public void Deleted()
        {
            this.State = Types.PersistableState.Deleted;
        }

        /// <summary>
        /// To set the version of the object
        /// </summary>
        /// <param name="value">version number</param>
        public void SetVersion(int? @value)
        {
            this.Version = @value;
        }

        public void LoadComposite<T>(string name, List<T> list)
        {
            dynamic composite;
            if (false == this.compositesIndexer.TryGetValue(name, out composite))
            {
                throw new Exception(string.Format("Property {0} is not indexed. Call IndexComposite() Before Load.", name));
            }

            composite.AddRange(list);
        }

        public void LoadCompositeItem<T>(string name, T item)
        {
            dynamic composite;
            if (false == this.compositesIndexer.TryGetValue(name, out composite))
            {
                throw new Exception(string.Format("Property {0} is not indexed. Call IndexComposite() Before Load.", name));
            }

            composite.Add(item);
        }

        /// <summary>
        /// To add a list of objects against a given name in a dictionary.
        /// </summary>
        /// <typeparam name="T">Type of the objects in the list</typeparam>
        /// <param name="name">Name against which the corresponding list of objects are added.</param>
        /// <param name="list">The List of objects to be added in the dictionary.</param>
        protected void IndexComposite<T>(string name, List<T> list)
        {
            this.compositesIndexer.Add(name, list);
        }

        public void CopyStateAndVersion(Persistable source)
        {
            this.SetVersion(source.Version);
            switch (source.State)
            {
                case PersistableState.Deleted:
                    this.Deleted();
                    break;

                case PersistableState.Modified:
                    this.Modified();
                    break;

                case PersistableState.New:
                    this.Added();
                    break;

                case PersistableState.Unchanged:
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}