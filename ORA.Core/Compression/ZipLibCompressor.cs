using System;
using System.IO;
using ORA.API.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace ORA.Core.Compression
{
    public class ZipLibCompressor : ICompressor
    {
        public byte[] Compress(byte[] data, int level)
        {
            var bos = new MemoryStream(data.Length);
            var compressor = new Deflater();
            compressor.SetLevel(level);
            compressor.SetInput(data);
            compressor.Finish();
            byte[] buf = new byte[1024];
            while (!compressor.IsFinished)
            {
                int count = compressor.Deflate(buf);
                bos.Write(buf, 0, count);
            }
            return bos.ToArray();
        }

        public byte[] Decompress(byte[] data)
        {
            var bos = new MemoryStream(data.Length);
            var inf = new Inflater();
            inf.SetInput(data);
            byte[] buf = new byte[1024];
            while (!inf.IsFinished)
            {
                int count = inf.Inflate(buf);
                bos.Write(buf, 0, count);
            }
            return bos.ToArray();
        }
    }
}
