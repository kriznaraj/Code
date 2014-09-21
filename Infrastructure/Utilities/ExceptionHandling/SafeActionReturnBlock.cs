using System;
using Controls.Logging;
using Controls.Types;

namespace Controls.ExceptionHandling
{
    internal sealed class SafeActionReturnBlock : ISafeActionReturnBlock
    {
        private readonly IExceptionManager exceptionManager;
        private readonly ILogger logger;
        private readonly int retryCount;

        internal SafeActionReturnBlock(ILogger logger, IExceptionManager exceptionManager, int retryCount)
        {
            this.logger = logger;
            this.exceptionManager = exceptionManager;
            this.retryCount = retryCount;
        }

        public TResult Invoke<TResult>(Action @do, Action onFailureDo, Action @finally, Func<TResult> resultFactory)
        {
            TResult result = (TResult)this.SafeInvoke<TResult>(@do, onFailureDo, @finally, resultFactory);
            return result;
        }

        internal TResult SafeInvoke<TResult>(Action @do, Action onFailureDo, Action @finally, Func<TResult> resultFactory)
        {
            TResult returnVal = default(TResult);
            int retryCount = 0;
            bool canExit = false;
            while (false == canExit)
            {
                try
                {
                    @do();
                    returnVal = resultFactory();

                    break;
                }
                catch (Exception exception)
                {
                    PostHandleAction handlerAction;
                    var handlerException = this.exceptionManager.Handle(exception, out handlerAction);

                    switch (handlerAction)
                    {
                        case PostHandleAction.Throw:
                            {
                                throw;
                            }
                        case PostHandleAction.Rethrow:
                            {
                                throw handlerException;
                            }
                        case PostHandleAction.Retry:
                            {
                                retryCount++;
                                if (retryCount > this.retryCount)
                                {
                                    this.logger.LogFatal(
                                        "SafeBlock",
                                        "Failed to execute Action Code block after " + this.retryCount + " retries. Exiting Code Block and Throwing exception. Exception caught is :" + exception.GetExceptionMessage());

                                    throw new SafeBlockException(
                                        "Failed to execute Action Code block after " + this.retryCount + " retries. Refer Inner Exception for more details",
                                        exception);
                                }
                                else
                                {
                                    this.logger.LogDebug(
                                        "SafeBlock",
                                        "Execute Action Code block for " + retryCount + " time(s). :" + exception.GetExceptionMessage());
                                }

                                continue;
                            }
                        case PostHandleAction.InvokeOnFailure:
                            {
                                if (onFailureDo != null)
                                {
                                    try
                                    {
                                        onFailureDo();
                                    }
                                    catch (Exception innerException)
                                    {
                                        this.logger.LogFatal("SafeBlock", innerException);
                                        throw new SafeBlockException("Failed to execute Fail safe code block. Refer to inner exception for more details", innerException);
                                    }
                                }
                                else
                                {
                                    this.logger.LogInfo("SafeBlock", "Invalid Fail Safe Code Block provided. Policy mandates to process fail safe. Exception reason for executing fail safe. " + exception.GetExceptionMessage());
                                    throw new SafeBlockException("Invalid Fail Safe Code Block provided. Policy mandates to process fail safe. See Inner exception for more details.", exception);
                                }

                                canExit = true;
                                break;
                            }

                        case PostHandleAction.Swallow:
                            {
                                this.logger.LogWarning("SafeBlock", "Exception handler returned exception to be swallowed. Exception is " + exception.GetExceptionMessage());
                                canExit = true;
                            }
                            break;

                        case PostHandleAction.Convert:
                            {
                                this.logger.LogDebug("SafeBlock", "Exception handle returned exception to be converted as return value. Exception is " + exception.GetExceptionMessage());
                                returnVal = (TResult)this.exceptionManager.Convert(exception);
                                canExit = true;
                            }

                            break;

                        case PostHandleAction.None:
                        default:
                            {
                                this.logger.LogFatal("SafeBlock", "Exception Handler not defined in the policy. Exception is " + exception.GetExceptionMessage());
                                throw new SafeBlockException("Exception Policy doesn't define an handler for the given exception. Refer Inner Exception for details", exception);
                            }
                    }
                }
                finally
                {
                    if (null != @finally)
                    {
                        @finally();
                    }
                }
            }

            return returnVal;
        }
    }
}