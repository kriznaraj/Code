using System.Collections.Generic;
using Controls.Framework.Interfaces;

namespace Controls.ControlLibrary
{
    internal class ControlSecurityFiller : ControlPropertyFiller
    {
        private IAccessPolicy GetAccess(List<Security> TaskCodes)
        {
            IAccessPolicy accessSecurity = new AccessPolicy();

            accessSecurity.Enabled = true;
            accessSecurity.ReadOnly = false;
            accessSecurity.Masking = false;
            accessSecurity.Visibility = true;

            if (TaskCodes != null && TaskCodes.Count > 0)
            {
                foreach (Security security in TaskCodes)
                {
                    bool securityCodeExist =this.IsAuthorized(security.TaskCode);
                    if (security.AccessType == AccessType.Enable)
                    {
                        accessSecurity.Enabled = securityCodeExist ? true : false;
                    }

                    if (security.AccessType == AccessType.Readonly)
                    {
                        accessSecurity.ReadOnly = securityCodeExist ? false : true;
                    }

                    if (security.AccessType == AccessType.Masking)
                    {
                        accessSecurity.Masking = securityCodeExist ? false : true;
                    }

                    if (security.AccessType == AccessType.Visible)
                    {
                        accessSecurity.Visibility = securityCodeExist ? true : false;
                    }
                }
            }
            return accessSecurity;
        }

        private void setDefalutSecurity(ControlPropertyBag propertyBag)
        {
            propertyBag.Enabled = true;
            propertyBag.ReadOnly = false;
            propertyBag.Visibility = true;
        }

        public override void Fill(TextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Masking = accessSecurity.Masking;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(NumericTextBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Masking = accessSecurity.Masking;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(LabelPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Masking = accessSecurity.Masking;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(PasswordBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(CheckBoxPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(RadioButtonPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(ButtonPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IAccessPolicy accessSecurity = null;
                if (fillerParams.UserTaskCodes != null)
                {
                    accessSecurity = GetAccess(fillerParams.UserTaskCodes);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(TextAreaPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null && propertyConfig.PropertyConfiguration != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(DateTimePropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(DropDownPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(CheckBoxListPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(RadioButtonListPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(TemplateListPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(GridPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(ShuttlePropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(TemplateDropDownPropertyBag propertyBag, FillerParams fillerParams)
        {
            if (fillerParams.SkipSecurityFill == false)
            {
                IModelPropertyConfiguration propertyConfig = ReadPropertyConfiguration(fillerParams.ModelName, fillerParams.PropertyName, fillerParams.ConfigKey);

                if (propertyConfig != null)
                {
                    IAccessPolicy accessSecurity = GetAccess(propertyConfig.PropertyConfiguration.Security);
                    if (accessSecurity != null)
                    {
                        propertyBag.Enabled = accessSecurity.Enabled;
                        propertyBag.ReadOnly = accessSecurity.ReadOnly;
                        propertyBag.Visibility = accessSecurity.Visibility;
                    }
                }
                else
                {
                    setDefalutSecurity(propertyBag);
                }
            }
            else
            {
                setDefalutSecurity(propertyBag);
            }
        }

        public override void Fill(DenomControlPropertyBag propertyBag, FillerParams fillerParams)
        {
            setDefalutSecurity(propertyBag);
        }
    }
}