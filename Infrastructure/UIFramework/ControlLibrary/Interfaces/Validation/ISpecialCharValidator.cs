
namespace Controls.ControlLibrary
{
    public interface ISpecialCharValidator
    {
        #region "Properties"

        string SpecialChars { get;  }

        RestrictionType Restriction { get;  }

        #endregion
    }
}
