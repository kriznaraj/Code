using Controls.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Resources;

namespace Controls.ResourceManager
{

    internal class DBResourceProvider : System.Resources.ResourceManager, IResourceProvider
    {
        /// <summary>
        /// Dictionary will hold the resource information based on culture.
        /// </summary>
        private Dictionary<string, ResourceSet> cultureResourceSetDictionary;

        /// <summary>
        /// This property decides which data provider to used to fetch the data.ex sqlite, sqlexpress, sqlCompact etc...
        /// </summary>
        private IDataProvider dataProvider;

        /// <summary>
        /// Constructor will initilize the Dictionary.
        /// </summary>
        public DBResourceProvider(IDataProvider dataProvider)
        {
            this.cultureResourceSetDictionary = new Dictionary<string, ResourceSet>(StringComparer.InvariantCultureIgnoreCase);
            this.dataProvider = dataProvider;
        }

        /// <summary>
        /// This method will return a resource set based on a Culture set by application.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="createIfNotExists"></param>
        /// <param name="tryParents"></param>
        /// <returns>ResourceSet</returns>
        protected override ResourceSet InternalGetResourceSet(System.Globalization.CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            ResourceSet rs = null;

            lock (this.cultureResourceSetDictionary)
            {
                if (false == this.cultureResourceSetDictionary.TryGetValue(culture.Name, out rs))
                {
                    rs = new ResourceSet(new DBResourceReader(culture, this.dataProvider));
                    this.cultureResourceSetDictionary[culture.Name] = rs;
                }
            }

            return rs;
        }


        /// <summary>
        /// Method restures string for given key from resources.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>string</returns>
        public string GetLiteral(string key, string DefaultValue = "")
        {

#if Debug

            if (string.IsNullOrEmpty(DefaultValue) == false)
            {
                using (IDbConnection dbConnection = this.dataProvider.GetConnection)
                {
                    dbConnection.Open();

                    string selectQuery = "SELECT * FROM tStringResource WHERE  ResourceKey = @ResourceKey";
                    List<KeyValuePair<string, object>> selectParameters = new List<KeyValuePair<string, object>>();
                    selectParameters.Add(new KeyValuePair<string, object>("@ResourceKey", key));
                    IDbCommand selectCommand = this.dataProvider.GetCommand(selectQuery, dbConnection, this.dataProvider.GetParameter(selectParameters), null);
                    DataTable dataTable = this.dataProvider.SelectQuery(selectCommand);
                   
                    if (dataTable != null && dataTable.Rows.Count == 0)
                    {
                        string strQuery = "INSERT INTO tStringResource (ResourceKey, ResourceValue, Language) VALUES (@ResourceKey, @ResourceValue, @Language)";
                        List<KeyValuePair<string, object>> resourceParameters = new List<KeyValuePair<string, object>>();
                        resourceParameters.Add(new KeyValuePair<string, object>("@ResourceKey", key));
                        resourceParameters.Add(new KeyValuePair<string, object>("@ResourceValue", DefaultValue));
                        resourceParameters.Add(new KeyValuePair<string, object>("@Language", System.Threading.Thread.CurrentThread.CurrentUICulture.Name));

                        IDbCommand iDbCommand = this.dataProvider.GetCommand(strQuery, dbConnection, this.dataProvider.GetParameter(resourceParameters), null);
                        bool success = this.dataProvider.ExecuteNonQuery(iDbCommand);
                        if (success)
                        {
                            this.InternalGetResourceSet(System.Threading.Thread.CurrentThread.CurrentUICulture, false, false);
                        }
                    }
                }
            }
#endif

            string retValue = this.GetString(key);

            if (string.IsNullOrEmpty(retValue))
            {
                retValue = key;
            }


            return retValue;

        }

        /// <summary>
        /// Method returns a currecy symbol for current UI culture.
        /// </summary>
        /// <returns>string</returns>
        public string GetCurrencySymbol()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencySymbol;
        }

        /// <summary>
        /// Returns a Image for given key from resources or Configured path.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Image GetImage(string key)
        {
            Image image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + key);
            return image;
        }


        /// <summary>
        /// Method add a resource to resources.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddResource(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
