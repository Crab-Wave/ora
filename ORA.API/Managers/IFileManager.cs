using System.IO;

namespace ORA.API.Managers
{
    public interface IFileManager
    {
        File[] GetOwnedFiles();

        File CreateFile(Cluster cluster, string realPath, string clusterPath);

        bool RemoveFile(Cluster cluster, File file) => this.RemoveFile(cluster, file.Hash);

        bool RemoveFile(Cluster cluster, string hash);

        string[] GetFiles(Cluster cluster);

        File GetFile(Cluster cluster, string hash);

        byte[] GetFileContent(Cluster cluster, string hash) =>
            this.GetFileContent(cluster, this.GetFile(cluster, hash));

        byte[] GetFileContent(Cluster cluster, File file);
    }
}
