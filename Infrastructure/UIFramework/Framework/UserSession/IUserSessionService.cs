using System;

namespace Controls.Framework
{
    interface IUserSessionService
    {
        ISessionContext Validate(string token);
        bool Authorize(IExecutionContext exeContext);
    }
}
