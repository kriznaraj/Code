using BallyTech.Infrastructure.Logging;
using BallyTech.Infrastructure.Session;
using BallyTech.Infrastructure.Types;
using BallyTech.Infrastructure.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.Authorization
{
    public class APISecurityProvider
    {
        private readonly ISemanticLog logger;
        private readonly IOperationSecurityConfigProvider securityConfigProvider;
        private readonly IAuthorize authorize;
        private readonly ISessionManager sessionManager;
        private readonly IExecutionContextProvider executionContextProvider;

        public APISecurityProvider(IOperationSecurityConfigProvider securityConfigProvider, ISemanticLog logger, ISessionManager sessionManager, IAuthorize authorize, IExecutionContextProvider executionContextProvider)
        {
            this.logger = logger;
            this.securityConfigProvider = securityConfigProvider;
            this.sessionManager = sessionManager;
            this.authorize = authorize;
            this.executionContextProvider = executionContextProvider;
        }

        public IResponse Validate<T>(T message) where T : IAPIRequestHeaderProvider
        {
            if (message == null)
            {
                throw new ArgumentNullException("message", "Message cannot be null");
            }

            IAPIRequestHeader header = message.Get();
            if (null != header)
            {
                throw new ArgumentNullException("header", "Header not defined for the incoming request");
            }

            if (String.IsNullOrWhiteSpace(header.APIName))
            {
                throw new ArgumentNullException("header.APIName", "APIName not defined in the header");
            }

            IOperationSecurityConfig securityConfig = this.securityConfigProvider.Get(header.APIName);
            Response response = new Response();
            if (securityConfig != null)
            {
                if (securityConfig.RequiresSession)
                {
                    IResponse<ISession> sessionContextResult = this.sessionManager.FindSession(header.SessionId);
                    if (sessionContextResult.Result == ResultType.Success)
                    {
                        IResponse<IUserContext> userContextResult = this.authorize.Authorize(sessionContextResult.Data, securityConfig);
                        if (userContextResult.Result == ResultType.Success)
                        {
                            ExecutionContextProvider.SetExecutionContext(this.executionContextProvider.GetExecutionContext(sessionContextResult.Data, userContextResult.Data, securityConfig));
                            response.Result = ResultType.Success;
                        }
                        else
                        {
                            response.Result = ResultType.Failure;
                            response.Params = userContextResult.Params;
                            response.Params.Add("APIName", header.APIName);
                            response.Params.Add("SessionId", header.SessionId);
                            response.Params.Add("SecurityCodeId", securityConfig.ID);
                            response.Params.Add("TranCodeId", securityConfig.OperationID);
                            response.ErrorCode = sessionContextResult.ErrorCode;
                            this.logger.Warning("User Security validation failed for the incoming request with session Id " + header.SessionId + ". API Name " + header.APIName);
                        }
                    }
                    else
                    {
                        response.Result = ResultType.Failure;
                        response.Params = sessionContextResult.Params;
                        response.Params.Add("APIName", header.APIName);
                        response.Params.Add("SessionId", header.SessionId);
                        response.ErrorCode = sessionContextResult.ErrorCode;
                        this.logger.Warning("Session validation failed for the incoming request with session Id " + header.SessionId + ". API Name " + header.APIName);
                    }
                }
                else
                {
                    this.logger.Log("Security validation is skipped for the API " + header.APIName + ". Validation is skipped response sent as unknown", LogType.Info);
                }
            }
            else
            {
                this.logger.Log("Validation config not defined for the API " + header.APIName + ". Validation is skipped response sent as unknown", LogType.Info);
            }

            return response;
        }
    }
}