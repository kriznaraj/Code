
namespace Controls.ControlLibrary
{
    public class Externalizer
    {
        public string GetExternalizedMessage(string key)
        {
            return ControlLibraryConfig.ResourceService.GetLiteral(key);
        }
    }
}
