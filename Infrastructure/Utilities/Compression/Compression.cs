using System.IO;
using System.IO.Compression;

namespace Controls.Compression
{
    public class Compression : ICompression
    {
        public Stream Deflate(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Compress, true);
        }

        public Stream Inflate(Stream stream)
        {
            return new GZipStream(stream, CompressionMode.Decompress, true);
        }
    }
}