namespace ORA.API.Compression
{
    /// <summary>
    ///     An object capable of compressing data or decompressing data that has been previously encrypted with the same algorithm.
    /// </summary>
    /// <example>
    ///     The following code
    ///     <code>
    ///         ICompressor comp = ...;
    ///         byte[] data = ...;
    ///         byte[] compressed = comp.Compress(data);
    ///
    ///         Console.WriteLine(comp.Decompress(encrypted).Equals(data));
    ///     </code>
    ///     will print the value true.
    /// </example>
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
