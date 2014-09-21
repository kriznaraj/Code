using System;
using System.Collections.Generic;

namespace Controls.Framework
{
    internal static class ControllerBag
    {
        private static readonly Dictionary<String, ControllerCreateParams> _nameCreateParamMap = new Dictionary<string, ControllerCreateParams>();

        internal static void Add(String name,ControllerCreateParams @params)
        {
            _nameCreateParamMap.Add(name, @params);
        }

        internal static ControllerCreateParams Get(String name)
        {
            if (_nameCreateParamMap.ContainsKey(name))
            {
                return _nameCreateParamMap[name];
            }
            else
            {
                FrameworkException frameException = new FrameworkException(3, "Invalid Configuration. Do you like to continue?");
                ControllerConfigurator.utilityProvider.GetLogger().LogFatal("ControllerBag.ControllerCreateParams", frameException);
                throw frameException;
               
            }
        }
    }
}
