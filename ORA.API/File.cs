namespace ORA.API
{
    public class File
    {
        public readonly string Cluster;
        public readonly string Path;
        public readonly string Hash;
        public readonly long Size;

        public File(string cluster, string path, string hash, long size)
        {
            this.Cluster = cluster;
            this.Path = path;
            this.Hash = hash;
            this.Size = size;
        }

        public override string ToString() =>
            @$"{{
    ""path"": ""{this.Path}"",
    ""hash"": ""{this.Hash}"",
    ""size"": {this.Size}
}}";
    }
}
