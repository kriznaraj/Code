
namespace Controls.ControlLibrary
{
    public interface IDatePropertyBag
    {
        string DateFormat { get; set; }
        int NumberOfMonths { get; }
        string MaxDate { get; }
        string MinDate { get; }
        bool ShowButtonPanel { get; }
        bool ChangeMonth { get; }
        bool ChangeYear { get; }
        bool ChangeDate { get; }
        string DateCssClass { get; }
    }
}
