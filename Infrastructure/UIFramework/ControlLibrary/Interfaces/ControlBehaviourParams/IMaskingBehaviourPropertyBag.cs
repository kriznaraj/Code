
namespace Controls.ControlLibrary
{
    public interface IMaskingBehaviourPropertyBag
    {

        #region "Properties"

        string MaskingChar { get; }        
        MaskingType MaskingType { get; }
        int MaskCharLength { get; }
        MaskingPosition MaskingPosition { get; }

        #endregion

    }
}
