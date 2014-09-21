using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Data;
using Controls.Data;
using System.Data.Common;
using System;
using System.Configuration;
using Controls.Framework.Interfaces;


namespace Controls.Framework.Extensions
{
    /// <summary>
    /// An extension of process command to provide authentication
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public abstract class LoginCommand<T, V> : ProcessCommand<T, V>
        where T : ViewModelBase
        where V : ViewModelBase
    {
        protected abstract bool Authenticate(T viewModel);
        protected abstract bool StartSession(T viewModel);
        protected abstract UserContext GetUserContext(T ViewModel);
        protected abstract UserRoleContext GetUserRoleContext();
        protected abstract V GetResponse();
        protected string SecurityToken { get; private set; }

        public sealed override V Process(IExecutionContext executionCtx, ISessionContext sessionCtx, T viewModel)
        {
            V response = default(V);
            if (sessionCtx == null)
            {
                if (Authenticate(viewModel))
                {
                    UserContext userContext = GetUserContext(viewModel);
                    this.SecurityToken = GenerateToken(userContext.SecurityToken);
                    sessionCtx = SaveUserContext(userContext);
                    UserRoleContext userRoleContext = GetUserRoleContext();
                    SaveUserRoleContext(userRoleContext, sessionCtx);
                }
            }
            else
            {
                if (StartSession(viewModel))
                {
                    UserRoleContext userRoleContext = GetUserRoleContext();
                    SaveUserRoleContext(userRoleContext, sessionCtx);
                }
            }
            response = GetResponse();

            return response;
        }

        /// <summary>
        /// Generate Token for the Particular session per user
        /// </summary>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        private string GenerateToken(string apiToken)
        {
            MD5 crypto = MD5.Create();
            return Security.GetHash(crypto, apiToken + HttpContext.Current.Session.SessionID);
        }

        /// <summary>
        /// Save the logged in user object
        /// </summary>
        /// <param name="userRoleContext"></param>
        /// <param name="sessionCtx"></param>
        private void SaveUserRoleContext(UserRoleContext userRoleContext, ISessionContext sessionCtx)
        {
            SessionContext sessionContext = SessionStore.Get<SessionContext>("SessionContext");
            if (sessionContext != null)
            {
                sessionContext.UserSecurity = userRoleContext.UserSecurity;
                sessionContext.UserTransactionLimit = userRoleContext.UserTransactionLimit;
                sessionContext.UserTask = new List<IUserTask>();
                if (userRoleContext.UserSecurity != null && userRoleContext.UserSecurity.Count > 0)
                {
                    foreach (UserTask userTask in userRoleContext.UserSecurity)
                    {
                        sessionContext.UserTask.Add(userTask);
                    }
                }
            }
            else
            {
                throw new FrameworkException(1, "Invalid Session");
            }
        }

        /// <summary>
        /// Accepts the usercontext object and set it in SessionContext and expose it as ISessionContext across Application
        /// </summary>
        /// <param name="userContext"></param>
        private SessionContext SaveUserContext(UserContext userContext)
        {
            SessionContext sessionContext = new SessionContext();
            sessionContext.SessionToken = userContext.SecurityToken;
            sessionContext.UserProfile = new UserProfile() { UserCultureInfo = userContext.UserCultureInfo };
            sessionContext.HostDevice = userContext.UserHostDevice;
            sessionContext.HostIP = userContext.HostIP;
            sessionContext.HostName = userContext.HostName;
            sessionContext.SetSiteConfig(GetSiteConfig());
            this.LoadLayoutConfig(sessionContext);
            SessionStore.Set<ISessionContext>("SessionContext", sessionContext);
            return sessionContext;
        }

        private void LoadLayoutConfig(SessionContext sessionContext)
        {
            sessionContext.LayoutConfig = new List<UILayoutConfig>();
            sessionContext.LayoutConfig.AddRange(UILayoutConfigOperation.GetLayoutConfig());
        }

        protected virtual List<ISiteConfigSetting> GetSiteConfig()
        {
            return null;
        }
    }
}
