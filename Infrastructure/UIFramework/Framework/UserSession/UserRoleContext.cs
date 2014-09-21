using System.Collections.Generic;

namespace Controls.Framework
{
    /*This Class used to store the Role/Tranlimit and other info for the logged in User.*/
    public class UserRoleContext
    {
        public List<object> UserSecurity { get; set; }
        public List<object> UserTransactionLimit { get; set; }
    }
}
