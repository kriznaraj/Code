using System;
using System.Drawing;

namespace Controls.ResourceManager
{
    public class FileResourceProvider : System.Resources.ResourceManager, IResourceProvider
    {
        /// <summary>
        /// Resource manager object.
        /// </summary>
        System.Resources.ResourceManager resourceManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FileResourceProvider(string baseName, System.Reflection.Assembly assembly)
            : base(baseName, assembly)
        {

        }

        /// <summary>
        /// Returns a string for given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetLiteral(string key, string DefaultValue = "")
        {
            string retValue = this.GetString(key);

            if (string.IsNullOrEmpty(retValue))
            {
                retValue = key;
            }

            return retValue;
        }

        /// <summary>
        /// Returns a  currency symbol for Current culture.
        /// </summary>
        /// <returns></returns>
        public string GetCurrencySymbol()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencySymbol;
        }

        /// <summary>
        /// Return a Image for given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Image GetImage(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a resource to resource storage.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddResource(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
