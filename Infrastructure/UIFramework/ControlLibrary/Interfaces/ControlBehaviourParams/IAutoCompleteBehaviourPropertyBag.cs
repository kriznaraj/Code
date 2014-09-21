
namespace Controls.ControlLibrary
{
    public interface IAutoCompleteBehaviourPropertyBag
    {
        #region "Properties"

        string ActionName { get; }
        string ControllerName { get; }
        string ActionURL { get; }
        int MinCharRequired { get; }
        int MaxResultCount { get; }
        OrderByType OrderBy { get; }
        SearchType SearchType { get; }

        #endregion      

    }
}
