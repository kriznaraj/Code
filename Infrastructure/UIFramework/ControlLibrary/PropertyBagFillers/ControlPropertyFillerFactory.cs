
namespace Controls.ControlLibrary
{
    internal class ControlPropertyFillerFactory
    {
        public static ControlPropertyFiller[] Get()
        {
            return new ControlPropertyFiller[] 
                    {   new ControlDefaultsFiller(), 
                        new ControlBehaviorFiller(),
                        new ControlValidationFiller(),
                        new ControlSecurityFiller(),
                        new ControlLocalizationFiller(),
                        new ControlOverriddenBehaviorFiller()
                    };
        }
    }
}