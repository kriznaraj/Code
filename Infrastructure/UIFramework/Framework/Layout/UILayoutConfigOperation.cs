using Controls.Data;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Controls.Framework
{
    public static class UILayoutConfigOperation
    {
        public static List<UILayoutConfig> GetLayoutConfig()
        {
            List<UILayoutConfig>  layoutConfig = new List<UILayoutConfig>();

            string strQuery = "Select TaskId, Module, GroupId, ElementId, MenuType, Controller, ActionName, Image, Caption, FullName, "
                                + "AccessKey, MenuSize, Flag, CreateDtm, CreatedBy, ModifiedDtm, ModifiedBy, ModuleImage, DataRowVersion , SerializeName,ValidateForm, RenderSection, OverideFunction,Align,TranCode,Isseparator,Ismandatory , ParentGroupId , TranAccount from tUILayoutConfig";
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageProviderConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);//ConfigProvider.ConfigHelper.GetDataProvider();
            using (IDbConnection dbConnection = iDataProvider.GetConnection)
            {
                dbConnection.Open();
                IDbCommand iDbCommand = iDataProvider.GetCommand(strQuery, dbConnection, null, CommandType.Text);
                DbDataReader dbDataReader = iDataProvider.ExecuteQuery(iDbCommand);
                //if (dbDataReader.HasRows)
                {
                    
                    while (dbDataReader.Read())
                    {
                        UILayoutConfig uiLayoutConfig = new UILayoutConfig();
                        uiLayoutConfig.TaskID = Convert.ToInt32(dbDataReader["TaskId"].ToString());
                        uiLayoutConfig.ModuleName = dbDataReader["Module"].ToString();
                        uiLayoutConfig.GroupId = Convert.ToInt32(dbDataReader["GroupId"].ToString());
                        uiLayoutConfig.ElementId = Convert.ToInt32(dbDataReader["ElementId"].ToString());
                        uiLayoutConfig.MenuType = dbDataReader["MenuType"].ToString();
                        uiLayoutConfig.ControllerName = dbDataReader["Controller"].ToString();
                        uiLayoutConfig.ActionName = dbDataReader["ActionName"].ToString();
                        uiLayoutConfig.Image = dbDataReader["Image"].ToString();
                        uiLayoutConfig.Caption = dbDataReader["Caption"].ToString();
                        uiLayoutConfig.FullName = dbDataReader["FullName"].ToString();
                        uiLayoutConfig.AccessKey = dbDataReader["AccessKey"].ToString();
                        uiLayoutConfig.Size = dbDataReader["MenuSize"].ToString();
                        uiLayoutConfig.IsModule = true;
                        uiLayoutConfig.Flag = true;
                        uiLayoutConfig.CreateDtm = Convert.ToDateTime(dbDataReader["CreateDtm"].ToString());
                        uiLayoutConfig.CreatedBy = Convert.ToInt32(dbDataReader["GroupId"].ToString());
                        uiLayoutConfig.ModifiedDtm = Convert.ToDateTime(dbDataReader["ModifiedDtm"].ToString());
                        uiLayoutConfig.ModifiedBy = Convert.ToInt32(dbDataReader["GroupId"].ToString());
                        uiLayoutConfig.ModuleImage = dbDataReader["ModuleImage"].ToString();
                        uiLayoutConfig.DataRowVersion = Convert.ToInt32(dbDataReader["DataRowVersion"].ToString());
                        uiLayoutConfig.SerializeName = (dbDataReader["SerializeName"] != null) ? dbDataReader["SerializeName"].ToString() : string.Empty;
                        uiLayoutConfig.ValidateForm = (dbDataReader["ValidateForm"] != null) ? Convert.ToBoolean(dbDataReader["ValidateForm"]) : false;
                        uiLayoutConfig.RenderSection = dbDataReader["RenderSection"].ToString();
                        uiLayoutConfig.OverideFunction = dbDataReader["OverideFunction"].ToString();
                        uiLayoutConfig.Align = (dbDataReader["Align"] != null) ? dbDataReader["Align"].ToString() : string.Empty;
                        uiLayoutConfig.TranCode = (dbDataReader["TranCode"] != null) ? dbDataReader["TranCode"].ToString() : string.Empty;
                        uiLayoutConfig.Isseparator = (dbDataReader["Isseparator"] != null) ? dbDataReader["Isseparator"].ToString() : string.Empty;
                        uiLayoutConfig.Ismandatory = (dbDataReader["Ismandatory"] != null) ? dbDataReader["Ismandatory"].ToString() : string.Empty;
                        uiLayoutConfig.ParentGroupId = (dbDataReader["ParentGroupId"] != null) ? dbDataReader["ParentGroupId"].ToString() : "0";
                        uiLayoutConfig.TranAccount = (dbDataReader["TranAccount"] != null) ? dbDataReader["TranAccount"].ToString() : "0";
                        layoutConfig.Add(uiLayoutConfig);
                    }

                    dbDataReader.Close();
                }
            }

            return layoutConfig;

        }

        public static void InsertUIConfigList(UILayoutConfig messageObject)
        {
            if (messageObject != null)
            {

                string InsertQuery = "INSERT INTO tUILayoutConfig (TaskId, Module, GroupId, ElementId, MenuType, Controller, ActionName, Image, Caption, FullName, AccessKey, MenuSize, Flag, CreateDtm, CreatedBy, ModifiedDtm, ModifiedBy, ModuleImage,DataRowVersion,SerializeName,ValidateForm, RenderSection, OverideFunction,Align,TranCode,Isseparator,Ismandatory , ParentGroupId , TranAccount) "
                + "VALUES(@TaskId, @Module, @GroupId, @ElementId, @MenuType, @Controller, @ActionName, @Image, @Caption,  @FullName, @AccessKey, @MenuSize, @Flag, @CreateDtm, @CreatedBy, @ModifiedDtm, @ModifiedBy,@ModuleImage, @DataRowVersion, @SerializeName, @ValidateForm, @RenderSection, @OverideFunction, @Align, @TranCode, @Isseparator, @Ismandatory, @ParentGroupId, @TranAccount )";

                string connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageProviderConnection"].ToString();
                IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);//ConfigProvider.ConfigHelper.GetDataProvider()
                using (IDbConnection iDbConnection = iDataProvider.GetConnection)
                {
                    iDbConnection.Open();
                    IDbCommand iDbcommand = iDataProvider.GetCommand(InsertQuery, iDbConnection, iDataProvider.GetParameter(FormParameters(messageObject)), CommandType.Text);
                    iDataProvider.ExecuteNonQuery(iDbcommand);
                }
            }
        }

        public static void UpdateUIConfigList(UILayoutConfig messageObject)
        {
            string updateQuery = "Update tUILayoutConfig Set TaskId = @TaskId, Module = @Module, GroupId = @GroupId, ElementId = @ElementId, MenuType=@MenuType, Controller=@Controller, ActionName=@ActionName, Image=@Image" +
                                 "Caption = @Caption, FullName=@FullName, AccessKey = @AccessKey, MenuSize = @MenuSize, Flag = @Flag, CreateDtm=@CreateDtm, CreatedBy=@CreatedBy, ModifiedDtm=@ModifiedDtm, ModifiedBy = @ModifiedBy " +
                                 "ModuleImage=@ModuleImage, DataRowVersion=@DataRowVersion, SerializeName=@SerializeName, ValidateForm=@ValidateForm , RenderSection = @RenderSection, OverideFunction = @OverideFunction,Align = @Align, TranCode = @TranCode, Isseparator = @Isseparator, Ismandatory = @Ismandatory, ParentGroupId = @ParentGroupId, TranAccount =@TranAccount";
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageProviderConnection"].ToString();
            IDataProvider iDataProvider = new SqlCEDataProvider(connectionString);//ConfigProvider.ConfigHelper.GetDataProvider()
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();
                IDbCommand iDbcommand = iDataProvider.GetCommand(updateQuery, iDbConnection, iDataProvider.GetParameter(FormParameters(messageObject)), CommandType.Text);
                iDataProvider.ExecuteNonQuery(iDbcommand);
            }
        }

        private static List<KeyValuePair<string, object>> FormParameters(UILayoutConfig messageObject)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@TaskId", messageObject.TaskID));
            parameters.Add(new KeyValuePair<string, object>("@Module", messageObject.ModuleName));
            parameters.Add(new KeyValuePair<string, object>("@GroupId", messageObject.GroupId));
            parameters.Add(new KeyValuePair<string, object>("@ElementId", messageObject.ElementId));
            int menuType;
            bool res = int.TryParse(messageObject.MenuType, out menuType);
            if (res == true)
            {
               /// Module module;
               // Enum.TryParse<Module>(menuType.ToString(), out module);
               // parameters.Add(new KeyValuePair<string, object>("@MenuType", module.ToString()));
            }
            else
            {
                parameters.Add(new KeyValuePair<string, object>("@MenuType", messageObject.MenuType));
            }
            parameters.Add(new KeyValuePair<string, object>("@Controller", messageObject.ControllerName == null ? string.Empty : messageObject.ControllerName));
            parameters.Add(new KeyValuePair<string, object>("@ActionName", messageObject.ActionName == null ? string.Empty : messageObject.ActionName));
            parameters.Add(new KeyValuePair<string, object>("@Image", messageObject.Image));
            parameters.Add(new KeyValuePair<string, object>("@Caption", messageObject.Caption));
            parameters.Add(new KeyValuePair<string, object>("@FullName", string.Empty));
            parameters.Add(new KeyValuePair<string, object>("@AccessKey", string.Empty));
            parameters.Add(new KeyValuePair<string, object>("@MenuSize", messageObject.Size));
            parameters.Add(new KeyValuePair<string, object>("@Flag", true));
            parameters.Add(new KeyValuePair<string, object>("@CreateDtm", DateTime.Now));
            parameters.Add(new KeyValuePair<string, object>("@CreatedBy", messageObject.CreatedBy));
            parameters.Add(new KeyValuePair<string, object>("@ModifiedDtm", DateTime.Now));
            parameters.Add(new KeyValuePair<string, object>("@ModifiedBy", messageObject.ModifiedBy));
            parameters.Add(new KeyValuePair<string, object>("@ModuleImage", messageObject.ModuleImage == null ? string.Empty : messageObject.ModuleImage));
            parameters.Add(new KeyValuePair<string, object>("@DataRowVersion", 1));
            parameters.Add(new KeyValuePair<string, object>("@SerializeName", messageObject.SerializeName));
            parameters.Add(new KeyValuePair<string, object>("@ValidateForm", messageObject.ValidateForm));
            parameters.Add(new KeyValuePair<string, object>("@RenderSection", messageObject.RenderSection));
            parameters.Add(new KeyValuePair<string, object>("@OverideFunction", messageObject.OverideFunction));
            parameters.Add(new KeyValuePair<string, object>("@Align", messageObject.Align));
            parameters.Add(new KeyValuePair<string, object>("@TranCode", messageObject.TranCode));
            parameters.Add(new KeyValuePair<string, object>("@Isseparator", messageObject.Isseparator));
            parameters.Add(new KeyValuePair<string, object>("@Ismandatory", messageObject.Ismandatory));
            parameters.Add(new KeyValuePair<string, object>("@ParentGroupId", messageObject.ParentGroupId));
            parameters.Add(new KeyValuePair<string, object>("@TranAccount", messageObject.TranAccount));
            
            return parameters;
        }

        
    }
}
