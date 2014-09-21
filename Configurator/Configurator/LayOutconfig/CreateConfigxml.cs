using Controls.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Configurator
{
    public class CreateConfigxml
    {
        public static void CreateConfigXml()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageProviderConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();
                string strQuery = "Select TaskId, Module, GroupId, ElementId, MenuType, Controller, ActionName, Image, Caption, FullName, "
                              + "AccessKey, MenuSize, Flag, CreateDtm, CreatedBy, ModifiedDtm, ModifiedBy, ModuleImage, DataRowVersion,SerializeName, ValidateForm, RenderSection , OverideFunction,Align,TranCode,  Isseparator, Ismandatory, ParentGroupId, TranAccount from tUILayoutConfig";

                IDbCommand command = iDataProvider.GetCommand(strQuery, iDbConnection, null, CommandType.Text);
                IDataReader resourceDataReader = iDataProvider.ExecuteQuery(command);
                WriteConfigXml(resourceDataReader);
            }
        }

        public static void SaveConfigXml()
        {

            DeleteLayoutConfigRecords();

            XmlTextReader xmlTextReader = new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + @"\Configurations\UILayoutConfig.xml");

            string InsertQuery = "INSERT INTO tUILayoutConfig (TaskId, Module, GroupId, ElementId, MenuType, Controller, ActionName, Image, Caption, FullName, AccessKey, MenuSize, Flag, CreateDtm, CreatedBy, ModifiedDtm, ModifiedBy, ModuleImage,DataRowVersion, SerializeName,ValidateForm, RenderSection, OverideFunction,Align,TranCode,  Isseparator, Ismandatory, ParentGroupId, TranAccount)"
              + "VALUES(@TaskId, @Module, @GroupId, @ElementId, @MenuType, @Controller, @ActionName, @Image, @Caption,  @FullName, @AccessKey, @MenuSize, @Flag, @CreateDtm, @CreatedBy, @ModifiedDtm, @ModifiedBy,@ModuleImage, @DataRowVersion,@SerializeName, @ValidateForm, @RenderSection, @OverideFunction,@Align,@TranCode,@Isseparator,@Ismandatory, @ParentGroupId, @TranAccount)";

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageProviderConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);//ConfigProvider.ConfigHelper.GetDataProvider()
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();
                while (xmlTextReader.Read())
                {
                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    switch (xmlTextReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (xmlTextReader.Name == "ConfigSections")
                            {
                                parameters.Add(new KeyValuePair<string, object>("@TaskId",string.IsNullOrEmpty(xmlTextReader.GetAttribute("TaskId"))  ? "0" : xmlTextReader.GetAttribute("TaskId")));
                                parameters.Add(new KeyValuePair<string, object>("@Module", xmlTextReader.GetAttribute("Module")));
                                parameters.Add(new KeyValuePair<string, object>("@GroupId", xmlTextReader.GetAttribute("GroupId")));
                                parameters.Add(new KeyValuePair<string, object>("@ElementId", xmlTextReader.GetAttribute("ElementId")));
                                parameters.Add(new KeyValuePair<string, object>("@MenuType", xmlTextReader.GetAttribute("MenuType")));
                                parameters.Add(new KeyValuePair<string, object>("@Controller", xmlTextReader.GetAttribute("Controller") == null ? string.Empty : xmlTextReader.GetAttribute("Controller")));
                                parameters.Add(new KeyValuePair<string, object>("@ActionName", xmlTextReader.GetAttribute("ActionName") == null ? string.Empty : xmlTextReader.GetAttribute("ActionName")));
                                parameters.Add(new KeyValuePair<string, object>("@Image", xmlTextReader.GetAttribute("Image")));
                                parameters.Add(new KeyValuePair<string, object>("@Caption", xmlTextReader.GetAttribute("Caption")));
                                parameters.Add(new KeyValuePair<string, object>("@FullName", string.Empty));
                                parameters.Add(new KeyValuePair<string, object>("@AccessKey", string.Empty));
                                parameters.Add(new KeyValuePair<string, object>("@MenuSize", xmlTextReader.GetAttribute("MenuSize")));
                                parameters.Add(new KeyValuePair<string, object>("@Flag", true));
                                parameters.Add(new KeyValuePair<string, object>("@CreateDtm", DateTime.Now.ToString()));
                                parameters.Add(new KeyValuePair<string, object>("@CreatedBy", "1"));
                                parameters.Add(new KeyValuePair<string, object>("@ModifiedDtm", DateTime.Now.ToString()));
                                parameters.Add(new KeyValuePair<string, object>("@ModifiedBy", 1));
                                parameters.Add(new KeyValuePair<string, object>("@ModuleImage", xmlTextReader.GetAttribute("ModuleImage") == null ? string.Empty : xmlTextReader.GetAttribute("ModuleImage")));
                                parameters.Add(new KeyValuePair<string, object>("@DataRowVersion", 1));
                                parameters.Add(new KeyValuePair<string, object>("@SerializeName", xmlTextReader.GetAttribute("SerializeName") == null ? string.Empty : xmlTextReader.GetAttribute("SerializeName")));
                                parameters.Add(new KeyValuePair<string, object>("@ValidateForm", false));
                                parameters.Add(new KeyValuePair<string, object>("@RenderSection", xmlTextReader.GetAttribute("RenderSection")));
                                parameters.Add(new KeyValuePair<string, object>("@OverideFunction", xmlTextReader.GetAttribute("OverideFunction") == null ? string.Empty : xmlTextReader.GetAttribute("OverideFunction")));
                                parameters.Add(new KeyValuePair<string, object>("@Align", xmlTextReader.GetAttribute("Align") == null ? string.Empty : xmlTextReader.GetAttribute("Align")));
                                parameters.Add(new KeyValuePair<string, object>("@TranCode", xmlTextReader.GetAttribute("TranCode") == null ? string.Empty : xmlTextReader.GetAttribute("TranCode")));
                                parameters.Add(new KeyValuePair<string, object>("@Isseparator", xmlTextReader.GetAttribute("Isseparator") == null ? string.Empty : xmlTextReader.GetAttribute("Isseparator")));
                                parameters.Add(new KeyValuePair<string, object>("@Ismandatory", xmlTextReader.GetAttribute("Ismandatory") == null ? string.Empty : xmlTextReader.GetAttribute("Ismandatory")));
                                parameters.Add(new KeyValuePair<string, object>("@ParentGroupId", xmlTextReader.GetAttribute("ParentGroupId") == null ? "0" : xmlTextReader.GetAttribute("ParentGroupId")));
                                parameters.Add(new KeyValuePair<string, object>("@TranAccount", xmlTextReader.GetAttribute("TranAccount") == null ? "0" : xmlTextReader.GetAttribute("TranAccount")));

                                IDbCommand iDbcommand = iDataProvider.GetCommand(InsertQuery, iDbConnection, iDataProvider.GetParameter(parameters), CommandType.Text);
                                iDataProvider.ExecuteNonQuery(iDbcommand);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void WriteConfigXml(IDataReader iDataReader)
        {
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.Formatting = Formatting.Indented;

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("LayoutConfig");


            if (iDataReader != null)
            {
                while (iDataReader.Read())
                {
                    xmlTextWriter.WriteStartElement("ConfigSections");
                    xmlTextWriter.WriteAttributeString("TaskId", iDataReader["TaskId"].ToString());
                    xmlTextWriter.WriteAttributeString("Module", iDataReader["Module"].ToString());
                    xmlTextWriter.WriteAttributeString("GroupId", iDataReader["GroupId"].ToString());
                    xmlTextWriter.WriteAttributeString("ElementId", iDataReader["ElementId"].ToString());
                    xmlTextWriter.WriteAttributeString("MenuType", iDataReader["MenuType"].ToString());
                    xmlTextWriter.WriteAttributeString("Controller", iDataReader["Controller"].ToString());
                    xmlTextWriter.WriteAttributeString("ActionName", iDataReader["ActionName"].ToString());
                    xmlTextWriter.WriteAttributeString("Image", iDataReader["Image"].ToString());
                    xmlTextWriter.WriteAttributeString("Caption", iDataReader["Caption"].ToString());
                    xmlTextWriter.WriteAttributeString("FullName", iDataReader["FullName"].ToString());
                    xmlTextWriter.WriteAttributeString("AccessKey", iDataReader["AccessKey"].ToString());
                    xmlTextWriter.WriteAttributeString("MenuSize", iDataReader["MenuSize"].ToString());
                    //xmlTextWriter.WriteAttributeString("IsModule", (iDataReader["IsModule"] != null) ? iDataReader["IsModule"].ToString() : "true");

                    xmlTextWriter.WriteAttributeString("CreateDtm", iDataReader["CreateDtm"].ToString());
                    xmlTextWriter.WriteAttributeString("ModifiedDtm", iDataReader["ModifiedDtm"].ToString());
                    xmlTextWriter.WriteAttributeString("ModuleImage", iDataReader["ModuleImage"].ToString());
                    xmlTextWriter.WriteAttributeString("DataRowVersion", iDataReader["DataRowVersion"].ToString());
                    xmlTextWriter.WriteAttributeString("SerializeName", iDataReader["SerializeName"] != null ? iDataReader["SerializeName"].ToString() : string.Empty);
                    xmlTextWriter.WriteAttributeString("ValidateForm", iDataReader["ValidateForm"] != null ? iDataReader["ValidateForm"].ToString() : "false");
                    xmlTextWriter.WriteAttributeString("RenderSection", iDataReader["RenderSection"] != null ? iDataReader["RenderSection"].ToString() : string.Empty);
                    xmlTextWriter.WriteAttributeString("OverideFunction", iDataReader["OverideFunction"] != null ? iDataReader["OverideFunction"].ToString() : string.Empty);
                    xmlTextWriter.WriteAttributeString("Align", iDataReader["Align"] != null ? iDataReader["Align"].ToString() : string.Empty);
                    xmlTextWriter.WriteAttributeString("TranCode", iDataReader["TranCode"] != null ? iDataReader["TranCode"].ToString() : string.Empty);
                    xmlTextWriter.WriteAttributeString("Isseparator", iDataReader["Isseparator"] != null ? iDataReader["Isseparator"].ToString() : string.Empty);
                    xmlTextWriter.WriteAttributeString("Ismandatory", iDataReader["Ismandatory"] != null ? "False" : "False");
                    xmlTextWriter.WriteAttributeString("ParentGroupId", iDataReader["ParentGroupId"] != null ? iDataReader["ParentGroupId"].ToString() : "0");
                    xmlTextWriter.WriteAttributeString("TranAccount", iDataReader["TranAccount"] != null ? iDataReader["TranAccount"].ToString() : "0");
                    xmlTextWriter.WriteEndElement();
                }
            }

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(stringWriter.ToString());
            xmlDocument.Save(@"D:\StarTeam\Coding\Source\Site Controller\UI.Web.Demo\Configurator\Configurations\UILayoutConfig.xml");

        }

        private static void DeleteLayoutConfigRecords()
        {
            string strDeleteUIConfig = "Delete From tUILayoutConfig";


            string connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageProviderConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);

            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();

                IDbCommand resourCommand = iDataProvider.GetCommand(strDeleteUIConfig, iDbConnection, null, CommandType.Text);
                resourCommand.ExecuteNonQuery();

            }
        }
    }
}
