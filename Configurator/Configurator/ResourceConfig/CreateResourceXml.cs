using Controls.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Configurator
{
    public class CreateResourceXml
    {
        public static void CreateStringResourceXml()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ResourceDbConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();
                string strStringResource = "Select ResourceKey, ResourceValue, Language from tStringResource";

                IDbCommand command = iDataProvider.GetCommand(strStringResource, iDbConnection, null, CommandType.Text);
                IDataReader resourceDataReader = iDataProvider.ExecuteQuery(command);
                WriteStringResourceXml(resourceDataReader);
            }
        }

        public static void CreateErrorResourceXml()
        {
            string strErrorResouce = "Select ErrorKey, ErrorMessage,Language from tErrorResource";

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ResourceDbConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();
                IDbCommand Errorcommand = iDataProvider.GetCommand(strErrorResouce, iDbConnection, null, CommandType.Text);
                IDataReader errorDataReader = iDataProvider.ExecuteQuery(Errorcommand);
                WriteErrorResourceXml(errorDataReader);
            }

        }

        private static void WriteStringResourceXml(IDataReader iDataReader)
        {
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.Formatting = Formatting.Indented;

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Resources");


            if (iDataReader != null)
            {
                while (iDataReader.Read())
                {
                    xmlTextWriter.WriteStartElement("Resource");
                    xmlTextWriter.WriteAttributeString("ResourceKey", iDataReader["ResourceKey"].ToString());
                    xmlTextWriter.WriteAttributeString("ResourceValue", iDataReader["ResourceValue"].ToString());
                    xmlTextWriter.WriteAttributeString("Language", iDataReader["Language"].ToString());
                    xmlTextWriter.WriteEndElement();
                }
            }

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(stringWriter.ToString());
            xmlDocument.Save(@"D:\StarTeam\Coding\Source\Site Controller\UI.Web.Demo\Configurator\Configurations\StringResources.xml");

        }

        private static void WriteErrorResourceXml(IDataReader iDataReader)
        {
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.Formatting = Formatting.Indented;

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("ErrorResources");

            if (iDataReader != null)
            {
                while (iDataReader.Read())
                {
                    xmlTextWriter.WriteStartElement("ErrorResource");
                    xmlTextWriter.WriteAttributeString("ErrorKey", iDataReader["ErrorKey"].ToString());
                    xmlTextWriter.WriteAttributeString("ErrorMessage", iDataReader["ErrorMessage"].ToString());
                    xmlTextWriter.WriteAttributeString("Language", iDataReader["Language"].ToString());
                    xmlTextWriter.WriteEndElement();
                }
            }

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(stringWriter.ToString());
            xmlDocument.Save(@"D:\StarTeam\Coding\Source\Site Controller\UI.Web.Demo\Configurator\Configurations\ErrorResources.xml");
        }
    }
}
