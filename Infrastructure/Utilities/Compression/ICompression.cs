using System.IO;

namespace Controls.Compression
{
    public interface ICompression
    {
        Stream Inflate(Stream stream);

        Stream Deflate(Stream stream);
    }
}