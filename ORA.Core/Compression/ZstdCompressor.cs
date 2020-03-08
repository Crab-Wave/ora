using ORA.API.Compression;
using ZstdNet;

namespace ORA.Core.Compression
{
    public class ZstdCompressor : ICompressor
    {
        public byte[] Compress(byte[] data, int level)
        {
            var compOptions = new CompressionOptions(level);
            using (var comp = new Compressor(compOptions))
            {
                data = comp.Wrap(data);
            }

            return data;
        }

        public byte[] Decompress(byte[] data)
        {
            using (var decomp = new Decompressor())
            {
                data = decomp.Unwrap(data);
            }

            return data;
        }
    }
}
