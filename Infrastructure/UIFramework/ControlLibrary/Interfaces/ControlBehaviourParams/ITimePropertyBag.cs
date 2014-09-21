
namespace Controls.ControlLibrary
{
    public interface ITimePropertyBag
    {
        bool ShowAmPm { get; set; }

        string TimeFormat { get; }

        string TimeCssClass { get; }

        bool ShowDuration { get; }

        int Step { get; set; }

    }
}
