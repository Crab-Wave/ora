namespace ORA.API.Compression
{
    public interface ICompressor
    {
        byte[] Compress(byte[] data) => this.Compress(data, 3);

        byte[] Compress(byte[] data, int level);

        byte[] Decompress(byte[] data);
    }
}
