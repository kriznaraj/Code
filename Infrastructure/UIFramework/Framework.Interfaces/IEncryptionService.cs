
namespace Controls.Framework.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEncryptionService
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}
