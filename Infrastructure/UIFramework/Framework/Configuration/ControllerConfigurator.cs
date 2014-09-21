using System;
using System.Collections;
using System.Collections.Generic;
using Controls.Configuration;
using Controls.Types;
using System.Linq;
using Controls.Utilities;
using Controls.ResourceManager;

namespace Controls.Framework
{
    public static class ControllerConfigurator
    {
        public static IUtilityProvider utilityProvider;
        public static IResourceService iResourceService;
        public static void Configure(IConfigService configService, IUtilityProvider _utilityProvider, IResourceService _iResourceSerive)
        {
            utilityProvider = _utilityProvider;
            iResourceService = _iResourceSerive;
            /*Call LoadCommandConfig Method to Load all the Command Configuration through ConfigService.*/
            IEnumerable<CommandConfig> commands = configService.Get<CommandConfig>("CommandTypeConfig");
            IEnumerable<CommandActionConfig> actionConfigList = configService.Get<CommandActionConfig>("CommandActionTypeConfig");

            foreach (CommandActionConfig actionConfig in actionConfigList)
            {
                ControllerCreateParams controllerConfig = new ControllerCreateParams();
                controllerConfig.Name = actionConfig.ActionKey;
                controllerConfig.AllowAnonymous = actionConfig.AllowAnonymous;
                controllerConfig.ExceptionPolicy = actionConfig.ExceptionPolicy;
                controllerConfig.TaskId = actionConfig.TaskId;
                CommandConfig cmd = commands.Where<CommandConfig>(o => o.CommandKey == actionConfig.CommandConfig).FirstOrDefault();
                if (cmd != null)
                {
                    Type commandType = Type.GetType(cmd.CommandUri);
                    controllerConfig.CommandType = commandType;


                    if (commandType != null)
                    {
                        while (commandType.Name != typeof(ProcessCommand<,>).Name && commandType.Name != typeof(ParameterizedActionCommand<>).Name && commandType.Name != typeof(RequestCommand<>).Name && commandType.Name != typeof(ExecutorCommand).Name)
                        {
                            commandType = commandType.BaseType;
                        }

                        Type[] param = commandType.GetGenericArguments();

                        if (commandType.Name == typeof(ProcessCommand<,>).Name)
                        {
                            controllerConfig.RequestViewModel = param[0];
                            controllerConfig.ReponseViewModel = param[1];
                            controllerConfig.ControllerType = typeof(Processor<,,>).MakeGenericType(
                                new Type[] { controllerConfig.CommandType, controllerConfig.RequestViewModel, controllerConfig.ReponseViewModel });
                        }
                        else if (commandType.Name == typeof(ParameterizedActionCommand<>).Name)
                        {
                            controllerConfig.RequestViewModel = param[0];
                            controllerConfig.ControllerType = typeof(ParamterizedExecutor<,>).MakeGenericType(
                                new Type[] { controllerConfig.CommandType, controllerConfig.RequestViewModel });
                        }
                        else if (commandType.Name == typeof(RequestCommand<>).Name)
                        {
                            controllerConfig.ReponseViewModel = param[0];
                            controllerConfig.ControllerType = typeof(Requestor<,>).MakeGenericType(
                                new Type[] { controllerConfig.CommandType, controllerConfig.ReponseViewModel });
                        }
                        else if (commandType.Name == typeof(ExecutorCommand).Name)
                        {
                            controllerConfig.ControllerType = typeof(Executor<>).MakeGenericType(
                                new Type[] { controllerConfig.CommandType });
                        }


                        controllerConfig.DivId = actionConfig.RefreshDiv;
                        controllerConfig.ViewName = actionConfig.ViewName;
                        ResultType result;
                        Enum.TryParse(actionConfig.ResultType, out result);
                        controllerConfig.ResultBuilder = result;
                        ControllerBag.Add(controllerConfig.Name, controllerConfig);
                    }
                    else
                    {
                        utilityProvider.GetLogger().LogFatal("Controller Configurator", 9000);
                    }
                }
                else
                {
                    utilityProvider.GetLogger().LogFatal("Controller Configurator", 9001);
                }
            }
        }
    }
}
