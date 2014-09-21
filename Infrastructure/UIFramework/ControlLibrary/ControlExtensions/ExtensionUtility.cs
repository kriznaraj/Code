using Controls.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Controls.ControlLibrary
{
    public static partial class ControlExtension
    {
        private static void GetPropertyNameAndValue<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, out string propertyName, out string modelName, out object value, out string errorMessage, out string configKey, bool checkErrorMessage = true)
        {
            propertyName = string.Empty;
            configKey= string.Empty;
            propertyName = ExpressionHelper.GetExpressionText(expression);

            modelName = htmlHelper.ViewData.Model.GetType().Name;

            value = expression.Value(htmlHelper.ViewData);

            errorMessage = string.Empty;
            var modelBase = htmlHelper.ViewData.Model as IModelState;
            if (checkErrorMessage && modelBase != null)
            {
                errorMessage = modelBase.GetErrorMessage(propertyName);
            }

            if (modelBase != null)
            {
                configKey = modelBase.ConfigurationKey;
            }
        }

        private static string GetMaskedString(string value, MaskingBehaviourPropertyBag maskingProperty)
        {
            StringBuilder maskedText = new StringBuilder();
            if (maskingProperty != null)
            {
                switch (maskingProperty.MaskingType)
                {
                    case MaskingType.Complete:
                        {
                            maskedText.Append(new string(Convert.ToChar(maskingProperty.MaskingChar), value.Length));
                            break;
                        }
                    case MaskingType.Partial:
                        {
                            if (maskingProperty.MaskCharLength >= value.Length)
                            {
                                maskedText.Append(new string(Convert.ToChar(maskingProperty.MaskingChar), value.Length));
                            }
                            else
                            {
                                if (maskingProperty.MaskingPosition == MaskingPosition.First)
                                {
                                    maskedText.Append(new string(Convert.ToChar(maskingProperty.MaskingChar), maskingProperty.MaskCharLength));
                                    maskedText.Append(value.Substring(maskingProperty.MaskCharLength));
                                }
                                else if (maskingProperty.MaskingPosition == MaskingPosition.Last)
                                {
                                    maskedText.Append(value.Substring(0, value.Length - maskingProperty.MaskCharLength));
                                    maskedText.Append(new string(Convert.ToChar(maskingProperty.MaskingChar), maskingProperty.MaskCharLength));
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
                return maskedText.ToString();
            }
            else
            {
                return value;
            }
        }

        private static void SetStyleSettings(StylePropertyBag style, Dictionary<string, string> overrideSettings)
        {
            if (style != null)
            {
                if (!string.IsNullOrEmpty(style.Height))
                {
                    overrideSettings.Add(ControlLibConstants.HEIGHT, style.Height);
                }
                if (!string.IsNullOrEmpty(style.Width))
                {
                    overrideSettings.Add(ControlLibConstants.WIDTH, style.Width);
                }
            }
        }

        public static string GetLiteral(this HtmlHelper htmlHelper, string key)
        {
            return ControlLibraryConfig.ResourceService.GetLiteral(key);
        }

        public static object Value<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression, ViewDataDictionary<TModel> viewData)
        {
            return ModelMetadata.FromLambdaExpression(expression, viewData).Model;
        }

        public static string GetControlID<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            string controlID = string.Empty;

            controlID = ExpressionHelper.GetExpressionText(expression);

            controlID = controlID.Replace("[", "_").Replace("]", "_").Replace(".", "_");

            return controlID;
        }        

        public static string GetCurrencySymbol(this HtmlHelper htmlHelper)
        {
            return ControlLibraryConfig.ResourceService.GetCurrencySymbol();
        }

        public static bool IsAuthorized(this HtmlHelper htmlHelper,string userTaskCode)
        {
            return IsAuthorized(userTaskCode);
        }

        public static Dictionary<string, bool> IsAuthorized(this HtmlHelper htmlHelper,List<string> userTaskCodes)
        {
            Dictionary<string, bool> returnDict = new Dictionary<string, bool>();
            foreach (string strUserTaskCode in userTaskCodes)
            {
                returnDict.Add(strUserTaskCode, IsAuthorized(strUserTaskCode));
            }

            return returnDict;
        }

        public static string GetJsonData(this HtmlHelper htmlHelper, string variableName, object data, bool scriptTagRequired = true)
        {
            StringBuilder jsonScriptData = new StringBuilder();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (scriptTagRequired)
            {
                jsonScriptData.Append("<script type=\"text/javascript\">"); 
            }

            jsonScriptData.Append(htmlHelper.Raw(string.Format("var {0} = {1};", variableName, serializer.Serialize(data))));

            if (scriptTagRequired)
            {
                jsonScriptData.Append("</script>"); 
            }

            return jsonScriptData.ToString();
        }

        private static bool IsAuthorized(string accessPolicyCode)
        {
            return ControlLibraryConfig.AccessPolicyProvider.Current.GetAccessPolicy(accessPolicyCode);
        }

    }
}