
namespace Controls.ControlLibrary
{
    internal static class ControlPropertyBagExtension
    {
        internal static void Accept(this ControlPropertyBag propertyBag, ControlPropertyFiller[] fillers)
        {
            foreach (var filler in fillers)
            {
                propertyBag.Accept(filler);
            }
        }
    }
}