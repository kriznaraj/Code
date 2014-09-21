using Controls.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Configurator
{
    public class SaveResourceXml
    {
        public static void SaveStringResourceXml()
        {
            DeleteResourceRecords();
            XmlTextReader xmlTextReader = new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + @"\Configurations\StringResources.xml");

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ResourceDbConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);
            string strQuery = "INSERT INTO tStringResource (ResourceKey, ResourceValue, Language) VALUES(@ResourceKey, @ResourceValue, @Language)";
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();

                while (xmlTextReader.Read())
                {
                    List<KeyValuePair<string, object>> resourceParameters = new List<KeyValuePair<string, object>>();

                    switch (xmlTextReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (xmlTextReader.Name == "Resource")
                            {
                                resourceParameters.Add(new KeyValuePair<string, object>("@ResourceKey", xmlTextReader.GetAttribute("ResourceKey")));
                                resourceParameters.Add(new KeyValuePair<string, object>("@ResourceValue", xmlTextReader.GetAttribute("ResourceValue")));
                                resourceParameters.Add(new KeyValuePair<string, object>("@Language", xmlTextReader.GetAttribute("Language")));
                            }
                            break;
                        default:
                            break;
                    }



                    if (resourceParameters != null && resourceParameters.Count > 0)
                    {
                        IDbCommand command = iDataProvider.GetCommand(strQuery, iDbConnection, iDataProvider.GetParameter(resourceParameters), CommandType.Text);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void SaveErrorResourceXml()
        {
            XmlTextReader xmlTextReader = new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + @"\Configurations\ErrorResources.xml");

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ResourceDbConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);
            string strQuery = "INSERT INTO tErrorResource (ErrorKey, ErrorMessage, Language) VALUES(@ErrorKey, @ErrorMessage, @Language)";
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();

                while (xmlTextReader.Read())
                {
                    List<KeyValuePair<string, object>> resourceParameters = new List<KeyValuePair<string, object>>();

                    switch (xmlTextReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (xmlTextReader.Name == "ErrorResource")
                            {
                                resourceParameters.Add(new KeyValuePair<string, object>("@ErrorKey", xmlTextReader.GetAttribute("ErrorKey")));
                                resourceParameters.Add(new KeyValuePair<string, object>("@ErrorMessage", xmlTextReader.GetAttribute("ErrorMessage")));
                                resourceParameters.Add(new KeyValuePair<string, object>("@Language", xmlTextReader.GetAttribute("Language")));
                            }
                            break;
                        default:
                            break;
                    }



                    if (resourceParameters != null && resourceParameters.Count > 0)
                    {
                        IDbCommand command = iDataProvider.GetCommand(strQuery, iDbConnection, iDataProvider.GetParameter(resourceParameters), CommandType.Text);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void DeleteResourceRecords()
        {
            string strDeleteStringResource = "Delete From tStringResource";
            string strDeleteErrorResource = "Delete From tErrorResource";

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ResourceDbConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);

            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();

                IDbCommand resourCommand = iDataProvider.GetCommand(strDeleteStringResource, iDbConnection, null, CommandType.Text);
                resourCommand.ExecuteNonQuery();

                IDbCommand errorCommand = iDataProvider.GetCommand(strDeleteErrorResource, iDbConnection, null, CommandType.Text);
                errorCommand.ExecuteNonQuery();
            }
        }
    }
}
