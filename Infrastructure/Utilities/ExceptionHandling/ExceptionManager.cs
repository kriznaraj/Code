using System;

namespace Controls.ExceptionHandling
{
    internal sealed class ExceptionManager : IExceptionManager
    {
        private readonly ExceptionPolicy exceptionPolicy;

        public ExceptionManager(ExceptionPolicy exceptionPolicy)
        {
            this.exceptionPolicy = exceptionPolicy;
        }

        Exception IExceptionManager.Handle(Exception exception, out PostHandleAction action)
        {
            action = PostHandleAction.None;
            var returnVal = exception;
            var exceptionFullName = exception.GetType().FullName;

            SortedExceptionConfigList<ExceptionHandlerConfig, IExceptionHandler> handlers;
            if (this.exceptionPolicy.HandlerMap.TryGetValue(exceptionFullName, out handlers))
            {
                foreach (var handler in handlers)
                {
                    bool handled;
                    var handlerException = handler.Value.Value.Handle(exception, out handled);
                    if (handled)
                    {
                        action = handler.Value.Key.HandlerAction;
                        returnVal = handlerException;
                        break;
                    }
                }
            }

            if (action == PostHandleAction.None && this.exceptionPolicy.HandlerMap.TryGetValue(typeof(Exception).FullName, out handlers))
            {
                foreach (var handler in handlers)
                {
                    bool handled;
                    var handlerException = handler.Value.Value.Handle(exception, out handled);
                    if (handled)
                    {
                        action = handler.Value.Key.HandlerAction;
                        returnVal = handlerException;
                        break;
                    }
                }
            }

            return returnVal;
        }

        object IExceptionManager.Convert(Exception exception)
        {
            var exceptionFullName = exception.GetType().FullName;
            object retVal = null;
            SortedExceptionConfigList<ExceptionConvertorConfig, IExceptionConvertor> handlers;
            if (this.exceptionPolicy.ConvertorMap.TryGetValue(exceptionFullName, out handlers))
            {
                foreach (var handler in handlers)
                {
                    bool handled;
                    var response = handler.Value.Value.Handle(exception, out handled);
                    if (handled)
                    {
                        retVal = response;
                    }
                }
            }

            if (retVal == null && this.exceptionPolicy.ConvertorMap.TryGetValue(typeof(Exception).FullName, out handlers))
            {
                foreach (var handler in handlers)
                {
                    bool handled;
                    var response = handler.Value.Value.Handle(exception, out handled);
                    if (handled)
                    {
                        retVal = response;
                    }
                }
            }

            return retVal;
        }
    }
}