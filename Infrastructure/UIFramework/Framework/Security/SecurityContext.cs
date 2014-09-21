
namespace Controls.Framework
{
    internal sealed class SecurityContext : ISecurityContext
    {
        public SecurityContext(string token)
        {
            this.SecurityToken = token;
        }
        public string SecurityToken
        {
            get;
            private set;
        }
    }
}
