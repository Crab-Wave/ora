namespace ORA.API.Compression
{
    public interface ICompressor
    {
        /// <summary>
        ///     Compresses the given data following a specific algorithm with a default compression level of 3.
        /// </summary>
        /// <param name="data">The data to compress</param>
        /// <returns>The compressed data</returns>
        byte[] Compress(byte[] data) => this.Compress(data, 3);

        /// <summary>
        ///     Compresses the given data following a specific algorithm with a given compression level.
        /// </summary>
        /// <param name="data">The data to compress</param>
        /// <param name="level">The compression level</param>
        /// <returns>The compressed data</returns>
        byte[] Compress(byte[] data, int level);

        /// <summary>
        ///     Decompresses the given data if it has previously been compressed using the same algorithm.
        /// </summary>
        /// <param name="data">The data to decompress</param>
        /// <returns>The decompressed data</returns>
        byte[] Decompress(byte[] data);
    }
}
