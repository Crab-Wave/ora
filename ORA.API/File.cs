namespace ORA.API
{
    public class File
    {
        public readonly string Path;
        public readonly string Hash;
        public readonly long Size;

        public File(string path, string hash, long size)
        {
            this.Path = path;
            this.Hash = hash;
            this.Size = size;
        }
    }
}
