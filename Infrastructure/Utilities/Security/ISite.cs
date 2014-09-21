using System;

namespace Controls.Security
{
    public interface ISite
    {
        string Code { get; }

        int ID { get; }

        string Name { get; }

        TimeZoneInfo TimeZone { get; }
    }
}