using System.Collections.Generic;

namespace Controls.ControlLibrary
{
    public interface IPropertyConfiguration
    {
        string Key { get; }

        AutoCompleteBehaviourPropertyBag AutoCompleteProperties { get; }

        MaskingBehaviourPropertyBag MaskingProperties { get; }

        string AccessPolicyCode { get; }

        List<ValidationBase> Validators { get; }

        List<Security> Security { get; }

        List<SiteConfig> SiteConfig { get; }

        DatePropertyBag DateProperties { get; }

        TimePropertyBag TimeProperties { get; }
    }
}