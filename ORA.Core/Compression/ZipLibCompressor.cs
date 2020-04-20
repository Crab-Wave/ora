using System;
using System.IO;
using ORA.API.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace ORA.Core.Compression
{
    public class ZipLibCompressor : ICompressor
    {

        private static int n;

        public byte[] Compress(byte[] data, int level)
        {
            n = data.Length;
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
            byte[] result = new byte[n];
            Inflater inf = new Inflater();
            inf.SetInput(data);
            int error = inf.Inflate(result, 0, n);
            if (error == 0)
            {
                throw new FileLoadException("The a section of the swf file could not be decompressed.");
            }
            return result;
        }
    }
}
