
namespace Controls.ControlLibrary
{
    public interface ICustomValidator
    {
        CustomValidationType ValidationType { get; }
        string Expression { get; }
    }
}
