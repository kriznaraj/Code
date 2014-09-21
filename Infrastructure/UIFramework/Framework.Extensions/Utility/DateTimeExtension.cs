using Controls.Security;
using System;

namespace Controls.Framework.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Considers the input parameter as DateTime object in logged in site's time zone, converts the same to UTC with the details available in the Session.
        /// </summary>
        /// <param name="siteDateTime">DateTime in logged in site's timezone</param>
        /// <returns></returns>
        public static DateTime ToUtc(this DateTime siteDateTime)
        {
            TimeZoneInfo loggedInSiteTimeZoneInfo = SessionStore.Get<ISession>("SessionInfo").LoggedInSite.TimeZone;
            return TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(siteDateTime, DateTimeKind.Unspecified), loggedInSiteTimeZoneInfo);
        }

        /// <summary>
        /// Considers the input parameter as DateTime object in UTC, converts the same to the logged in site's time zone with the details available in the Session.
        /// </summary>
        /// <param name="utcDateTime">DateTime in UTC</param>
        /// <returns></returns>
        public static DateTime ToSiteTime(this DateTime utcDateTime)
        {
            TimeZoneInfo loggedInSiteTimeZoneInfo = SessionStore.Get<ISession>("SessionInfo").LoggedInSite.TimeZone;
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(utcDateTime, DateTimeKind.Unspecified), loggedInSiteTimeZoneInfo);
        }
    }
}
