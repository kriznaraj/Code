using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Controls.Types;

namespace Controls.Framework
{
    public class ExceptionConfigurator
    {
        public static void Configure(string configXMLFile)
        {

            string fileName = AppDomain.CurrentDomain.BaseDirectory + configXMLFile;

            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            XmlNodeList frameworkException = xmlDocument.SelectNodes(@"//Exceptions/Exception");

            foreach (XmlNode exceptionDetails in frameworkException)
            {
                IExceptionConfig exceptionConfig = null;
                string resultType = exceptionDetails.SelectSingleNode("Response").Attributes["Type"].Value;
                
                if (Enum.GetName(typeof(ResultType), ResultType.JSON) == resultType)
                {
                    exceptionConfig = new JsonException();
                    if (exceptionConfig.ResponseType == ResultType.JSON)
                    {
                        XmlNode jsonDetails = exceptionDetails.SelectSingleNode("Json");
                        ((JsonException)exceptionConfig).Type = (DisplayType)Int16.Parse(jsonDetails.Attributes["Type"].Value);

                        if (jsonDetails.ChildNodes.Count > 0)
                        {
                            ((JsonException)exceptionConfig).ActionConfig = new List< ExceptionActionConfig>();

                            foreach (XmlNode exceptionCommand in jsonDetails)
                            {
                                ExceptionActionConfig actionConfig = new ExceptionActionConfig();
                                string commandName = exceptionCommand.Attributes["Name"].Value;
                                actionConfig.Name = commandName;// To get Externalized String
                                actionConfig.URI = exceptionCommand.Attributes["URI"].Value;
                                actionConfig.Function = exceptionCommand.Attributes["Function"].Value;
                                ((JsonException)exceptionConfig).ActionConfig.Add( actionConfig);
                            }
                        }
                    }
                }
                else
                {
                    exceptionConfig = new ViewException();
                    XmlNode viewDetails = exceptionDetails.SelectSingleNode("View");
                    ((ViewException)exceptionConfig).ViewName = viewDetails.Attributes["Name"].Value;
                }
                exceptionConfig.ErrorId = Int64.Parse(exceptionDetails.Attributes["Id"].Value);
                ExceptionBag.Add(exceptionConfig.ErrorId, exceptionConfig);
            }
        }
    }
}
