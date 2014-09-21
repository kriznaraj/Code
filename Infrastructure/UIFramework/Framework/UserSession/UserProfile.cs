namespace Controls.Framework
{
    /*This Class used to stored the User Preference/Settings*/
    public class UserProfile : IUserProfile
    {
        public System.Globalization.CultureInfo UserCultureInfo
        {
            get;
            set;
        }

        public string ThemeName
        {
            get;
            set;
        }
    }
}
