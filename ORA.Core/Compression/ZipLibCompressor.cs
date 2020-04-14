using ORA.API.Compression;

namespace ORA.Core.Compression
{
    public class ZipLibCompressor : ICompressor
    {
        public byte[] Compress(byte[] data, int level) => throw new System.NotImplementedException();

        public byte[] Decompress(byte[] data) => throw new System.NotImplementedException();
    }
}
