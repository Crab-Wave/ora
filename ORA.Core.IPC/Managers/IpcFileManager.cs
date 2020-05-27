using JKang.IpcServiceFramework;
using ORA.API;
using ORA.API.Managers;

namespace ORA.Core.IPC.Managers
{
    public class IpcFileManager : IFileManager
    {
        private IpcServiceClient<IFileManager> _client;

        public IpcFileManager(IpcServiceClient<IFileManager> client)
        {
            this._client = client;
        }

        public File[] GetOwnedFiles() => this._client.InvokeAsync(manager => manager.GetOwnedFiles()).Result;

        public File CreateFile(Cluster cluster, string realPath, string clusterPath) => this._client
            .InvokeAsync(manager => manager.CreateFile(cluster, realPath, clusterPath)).Result;

        public bool RemoveFile(Cluster cluster, string hash) => this._client
            .InvokeAsync(manager => manager.RemoveFile(cluster, hash)).Result;

        public string[] GetFiles(Cluster cluster) =>
            this._client.InvokeAsync(manager => manager.GetFiles(cluster)).Result;

        public File GetFile(Cluster cluster, string hash) =>
            this._client.InvokeAsync(manager => manager.GetFile(cluster, hash)).Result;

        public byte[] GetFileContent(Cluster cluster, File file) =>
            this._client.InvokeAsync(manager => manager.GetFileContent(cluster, file)).Result;
    }
}
