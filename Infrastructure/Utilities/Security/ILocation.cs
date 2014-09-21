namespace Controls.Security
{
    /// <summary>
    /// Interface to be implemented by the location
    /// </summary>
    public interface ILocation
    {
        long AssetId { get; }

        string Code { get; }

        int ID { get; }

        string Name { get; }
    }
}