using System;
using System.Web;

namespace Controls.Framework
{
    /*This Class used to Get/Set the Session Values*/
    public static class SessionStore
    {
        public static T Get<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        public static void Set<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}
