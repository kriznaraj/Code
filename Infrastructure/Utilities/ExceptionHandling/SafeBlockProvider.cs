using System;
using System.Collections.Generic;
using Controls.Configuration;
using Controls.Logging;
using Controls.Types;

namespace Controls.ExceptionHandling
{
    public class SafeBlockProvider : ISafeBlockProvider
    {
        private readonly Dictionary<string, ExceptionPolicy> exceptionPolicies;
        private readonly ILogger logger;
        private readonly int retryCount;

        public SafeBlockProvider(IConfigService configService, ILogger logger)
        {
            var exceptionHandlePolicies = configService.Get<ExceptionHandlePolicy>("ExceptionHandlePolicy");
            this.logger = logger;
            this.retryCount = configService.Get<int>("ExceptionPolicy", "RetryCount");
            this.exceptionPolicies = new Dictionary<string, ExceptionPolicy>();
            this.Load(exceptionHandlePolicies);
        }

        public ISafeActionBlock Create(string exceptionHandlePolicy)
        {
            ExceptionPolicy exceptionPolicy;
            if (this.exceptionPolicies.TryGetValue(exceptionHandlePolicy, out exceptionPolicy))
            {
                IExceptionManager exceptionManager = new ExceptionManager(exceptionPolicy);
                return new SafeActionBlock(this.logger, exceptionManager, this.retryCount);
            }
            else
            {
                throw new SafeBlockException("Invalid Safe block policy provided for creating safe block",
                    new ArgumentOutOfRangeException("policy", exceptionHandlePolicy));
            }
        }

        public ISafeActionReturnBlock CreateResult(string exceptionHandlePolicy)
        {
            ExceptionPolicy exceptionPolicy;
            if (this.exceptionPolicies.TryGetValue(exceptionHandlePolicy, out exceptionPolicy))
            {
                IExceptionManager exceptionManager = new ExceptionManager(exceptionPolicy);
                return new SafeActionReturnBlock(this.logger, exceptionManager, this.retryCount);
            }
            else
            {
                throw new SafeBlockException("Invalid Safe block policy provided for creating safe block",
                    new ArgumentOutOfRangeException("policy", exceptionHandlePolicy));
            }
        }

        private void Load(IEnumerable<ExceptionHandlePolicy> exceptionHandlePolicies)
        {
            foreach (var item in exceptionHandlePolicies)
            {
                var handlers = new ExceptionHandlerMap();
                foreach (var handlerConfig in item.HandlerConfigList)
                {
                    SortedExceptionConfigList<ExceptionHandlerConfig, IExceptionHandler> priorityHandlers;
                    var fullName = handlerConfig.ExceptionType.FullName;
                    if (false == handlers.TryGetValue(fullName, out priorityHandlers))
                    {
                        priorityHandlers = new SortedExceptionConfigList<ExceptionHandlerConfig, IExceptionHandler>();
                        handlers.Add(fullName, priorityHandlers);
                    }

                    priorityHandlers.Add(
                        handlerConfig.InvokeSequence,
                        new KeyValuePair<ExceptionHandlerConfig, IExceptionHandler>(
                            handlerConfig, TypeFactory.CreateInstance<IExceptionHandler>(handlerConfig.Type)));
                }

                var convertors = new ExceptionConvertorMap();
                foreach (var convertorConfig in item.ConvertorConfigList)
                {
                    SortedExceptionConfigList<ExceptionConvertorConfig, IExceptionConvertor> priorityConvertors;
                    var fullName = convertorConfig.ExceptionType.FullName;
                    if (false == convertors.TryGetValue(fullName, out priorityConvertors))
                    {
                        priorityConvertors = new SortedExceptionConfigList<ExceptionConvertorConfig, IExceptionConvertor>();
                        convertors.Add(fullName, priorityConvertors);
                    }

                    priorityConvertors.Add(
                        convertorConfig.InvokeSequence,
                        new KeyValuePair<ExceptionConvertorConfig, IExceptionConvertor>(
                            convertorConfig, TypeFactory.CreateInstance<IExceptionConvertor>(convertorConfig.Type)));
                }

                this.exceptionPolicies.Add(item.Name, new ExceptionPolicy(convertors, handlers));
            }
        }
    }
}