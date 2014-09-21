using System;
using System.Globalization;

namespace Controls.Framework
{
    public interface IUserProfile
    {
        CultureInfo UserCultureInfo { get; }

        string ThemeName { get; }
    }
}
