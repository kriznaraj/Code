using Controls.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;


namespace Controls.ResourceManager
{
    internal class DBResourceReader : IResourceReader
    {
        private CultureInfo culture;

        private IDataProvider dataProvider;

        public DBResourceReader(CultureInfo culture, IDataProvider dataProvider)
        {
            this.culture = culture;
            this.dataProvider = dataProvider;
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            Hashtable keyValues = this.SelectExecuteQuery(this.culture.Name);
            return keyValues.GetEnumerator();
        }

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Hashtable SelectExecuteQuery(string cultureName)
        {
            Hashtable hashTable = new Hashtable();
            using (IDbConnection dbConnection = this.dataProvider.GetConnection)
            {
                dbConnection.Open();

                string resourceCmdText = "Select ResourceKey, ResourceValue from tStringResource where Language = @cultureName";

                List<KeyValuePair<string, object>> resourceParameters = new List<KeyValuePair<string, object>>();
                resourceParameters.Add(new KeyValuePair<string, object>("@cultureName", cultureName));
                IDbCommand command = this.GetCommand(resourceCmdText, dbConnection, this.dataProvider.GetParameter(resourceParameters), null);
                DataTable resourceDataTable = this.dataProvider.SelectQuery(command);


                string errorCmdText = "Select ErrorKey, ErrorMessage from tErrorResource where Language = @cultureName";

                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>("@cultureName", cultureName));
                IDbCommand Errorcommand = this.GetCommand(errorCmdText, dbConnection, this.dataProvider.GetParameter(parameters), null);
                DataTable ErrorDataTable = this.dataProvider.SelectQuery(Errorcommand);


                if (resourceDataTable != null && resourceDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in resourceDataTable.Rows)
                    {
                        if (hashTable.Contains(row["ResourceKey"].ToString()) == false)
                        {
                            hashTable.Add(row["ResourceKey"].ToString(), row["ResourceValue"].ToString());
                        }
                    }
                }

                if (ErrorDataTable != null && ErrorDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in ErrorDataTable.Rows)
                    {
                        if (hashTable.Contains(row["ErrorKey"].ToString()) == false)
                        {
                            hashTable.Add(row["ErrorKey"].ToString(), row["ErrorMessage"].ToString());
                        }
                    }
                }
            }

            return hashTable;
        }

        private void InsertQuery()
        {
            using (IDbConnection dbConnection = this.dataProvider.GetConnection)
            {
                    
            }
        }

        private IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter, IDbTransaction dbTransaction)
        {
            return this.dataProvider.GetCommand(commandText, dbConnection, dataParameter, dbTransaction, CommandType.Text);
        }

        private IDbCommand GetCommand(string commandText, IDbConnection dbConnection, IDataParameter[] dataParameter)
        {
            return this.dataProvider.GetCommand(commandText, dbConnection, dataParameter, CommandType.Text);
        }
    }
}
