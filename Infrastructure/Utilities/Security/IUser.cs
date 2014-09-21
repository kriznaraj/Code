using System;

namespace Controls.Security
{
    public interface IUser
    {
        /*
        * User Aggregate Id -Should be used as reference Key
        */

        String FirstName
        {
            get;
        }

        int ID
        {
            get;
        }

        bool IsActive
        {
            get;
        }

        bool IsLocked
        {
            get;
        }

        String LastName
        {
            get;
        }

        int LoginId
        {
            get;
        }

        string LoginName
        {
            get;
        }

        String MiddleName
        {
            get;
        }

        /*
        * Max 5 digit Numeric Id(Unique) Used for ticket print
        */
    }
}