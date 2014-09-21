namespace Controls.IDGeneration
{
    public interface IIDRangeRepository
    {
        IDRange GetNextRange(string key);
    }
}