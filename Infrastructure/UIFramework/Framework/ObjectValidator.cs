using Controls.ControlLibrary;
using Controls.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Controls.Framework
{
    public interface IPropertyNameProvider
    {
        string GetCurrentPropertyName();

        string PropertyName { get; set; }
    }

    internal class ObjectValidator : IPropertyNameProvider
    {
        public List<KeyValuePair<string, string>> ErrorList { get; set; }

        public string ViewName { get; set; }

        public string ConfigKey { get; set; }

        public string TypeName { get; set; }

        private readonly object data;

        IPropertyNameProvider PropertyNameProvider { get; set; }

        public string PropertyName { get; set; }

        public string GetCurrentPropertyName()
        {
            return (this.PropertyNameProvider == null) ? this.PropertyName : this.PropertyNameProvider.GetCurrentPropertyName() + "_" + this.PropertyName;       
        }


        public ObjectValidator(object data, IPropertyNameProvider propertyNameProvider, string configKey)
        {
            if (null == data)
            {
                throw new ArgumentNullException();
            }

            this.PropertyNameProvider = propertyNameProvider;

            this.data = data;
            ConfigKey = configKey;// this.data.GetType().Name;
            TypeName = this.data.GetType().Name;
            this.ErrorList = new List<KeyValuePair<string, string>>();
        }

        public bool Validate()
        {
            IDictionary<string, ModelPropertyConfiguration> modelPropConfig = ControlLibraryConfig.ControlConfigReader.GetModelConfigurationSettings(TypeName, this.ConfigKey);

            if (modelPropConfig != null)
            {
                foreach (KeyValuePair<string, ModelPropertyConfiguration> keyValue in modelPropConfig)
                {
                    this.PropertyName = keyValue.Key;

                    if (keyValue.Value.IsComplexType)
                    {
                        
                        if (keyValue.Value.IsEnumerable)
                        {
                            IEnumerable enumerable = GetNestedPropertyValue(this.data, keyValue.Key) as IEnumerable;
                            if (enumerable != null)
                            {
                                IEnumerator enumerator = enumerable.GetEnumerator();

                                int currentIndex = 0;

                                while (enumerator.MoveNext())
                                {
                                    if (enumerator.Current != null)
                                    {
                                        this.PropertyName = string.Format("{0}_{1}_",keyValue.Key,currentIndex);
                                        ObjectValidator validator = new ObjectValidator(enumerator.Current, this, ConfigKey);
                                        if (false == validator.Validate())
                                        {
                                            this.ErrorList.AddRange(validator.ErrorList);
                                        }
                                    }
                                    currentIndex++;
                                }
                            }
                        }
                        else
                        {
                            object data = GetNestedPropertyValue(this.data, keyValue.Key);
                            if (null != data)
                            {
                                ObjectValidator validator = new ObjectValidator(data, this, ConfigKey);
                                if (false == validator.Validate())
                                {
                                    this.ErrorList.AddRange(validator.ErrorList);
                                }
                            }
                        }
                    }
                    else
                    {
                        //PropertyInfo info = this.GetType().GetProperty(keyValue.Key);
                        object value = GetNestedPropertyValue(this.data, keyValue.Key);
                        IModelPropertyConfiguration imodel = modelPropConfig[keyValue.Key];
                        if (imodel != null && imodel.PropertyConfiguration != null && imodel.PropertyConfiguration.Validators != null && imodel.PropertyConfiguration.Validators.Count > 0)
                        {
                            foreach (IValidator item in imodel.PropertyConfiguration.Validators)
                            {
                                if (item.Type == ValidatorsType.Custom)
                                {
                                    CustomValidator validation = item as CustomValidator;
                                    CustomValidationExpressionConfiguration expressionConfig = ControlLibraryConfig.ControlConfigReader.GetCustomValidationExpressionConfiguration(validation.ValidationType.ToString());
                                    string _exression = string.Empty;
                                    if (expressionConfig != null)
                                    {
                                        _exression = expressionConfig.Expression;
                                    }
                                    if (item.Validate(value, _exression) == false)
                                    {
                                        if (ErrorList == null)
                                        {
                                            ErrorList = new List<KeyValuePair<string, string>>();
                                        }
                                        ErrorList.Add(new KeyValuePair<string, string>(this.GetCurrentPropertyName(), item.Message));
                                    }
                                }
                                else if (item.Type == ValidatorsType.RegExp)
                                {
                                    RegExValidator validation = item as RegExValidator;
                                    string _exression = validation.RegExpression;

                                    if (item.Validate(value, _exression) == false)
                                    {
                                        if (ErrorList == null)
                                        {
                                            ErrorList = new List<KeyValuePair<string, string>>();
                                        }
                                        ErrorList.Add(new KeyValuePair<string, string>(this.GetCurrentPropertyName(), item.Message));
                                    }
                                }
                                else if (item.Type == ValidatorsType.Required)
                                {
                                    bool mandatory = true;
                                    if (imodel.PropertyConfiguration.SiteConfig != null && imodel.PropertyConfiguration.SiteConfig.Count > 0)
                                    {
                                        bool? configVlaueVisible = this.GetSiteConfigValue(imodel.PropertyConfiguration.SiteConfig, SiteConfigType.Visible);
                                        if (configVlaueVisible.HasValue && configVlaueVisible == false)
                                        {
                                            mandatory = false;
                                        }

                                        if (mandatory)
                                        {
                                            bool? configVlaueMandatory = this.GetSiteConfigValue(imodel.PropertyConfiguration.SiteConfig, SiteConfigType.Mandatory);
                                            if (configVlaueMandatory.HasValue && configVlaueMandatory == false)
                                            {
                                                mandatory = false;
                                            }
                                        }
                                    }
                                    if (mandatory)
                                    {
                                        if (item.Validate(value, string.Empty) == false)
                                        {
                                            if (ErrorList == null)
                                            {
                                                ErrorList = new List<KeyValuePair<string, string>>();
                                            }
                                            ErrorList.Add(new KeyValuePair<string, string>(this.GetCurrentPropertyName(), item.Message));
                                        } 
                                    }
                                }
                                else
                                {
                                    if (item.Validate(value, string.Empty) == false)
                                    {
                                        if (ErrorList == null)
                                        {
                                            ErrorList = new List<KeyValuePair<string, string>>();
                                        }
                                        ErrorList.Add(new KeyValuePair<string, string>(this.GetCurrentPropertyName(), item.Message));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return this.ErrorList == null ? true : this.ErrorList.Count == 0;
        }

        public string GetErrorMessage(string propertyName)
        {
            string resultStr = string.Empty;
            if (this.ErrorList != null && this.ErrorList.Count > 0)
            {
                var a = this.ErrorList.Where(o => o.Key == propertyName).FirstOrDefault();
                resultStr = a.Value;
            }
            return resultStr;
        }

        public object GetNestedPropertyValue(object obj, string property)
        {
            if (string.IsNullOrEmpty(property))
                return string.Empty;

            var propertyNames = property.Split('.');

            foreach (var p in propertyNames)
            {
                if (obj == null)
                    return string.Empty;

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(p);
                if (info == null)
                    return string.Empty;

                obj = info.GetValue(obj, null);

            }

            return obj;
        }

        private bool? GetSiteConfigValue(List<SiteConfig> siteConfigList, SiteConfigType configType)
        {
            if (siteConfigList != null && siteConfigList.Count > 0)
            {
                var siteConfig = siteConfigList.Find(o => o.SiteConfigType == configType);
                if (siteConfig != null)
                {
                    ISiteConfigSetting siteConfigSetting = ControlLibraryConfig.SiteConfigProvider.Current.GetSiteConfig(siteConfig.ConfigKey);
                    if (siteConfigSetting != null)
                    {
                        return siteConfigSetting.ConfigValue;
                    }
                }
            }
            return null;
        }
    }
}
