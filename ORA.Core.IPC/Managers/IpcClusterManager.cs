using JKang.IpcServiceFramework;
using ORA.API;
using ORA.API.Managers;

namespace ORA.Core.IPC.Managers
{
    public class IpcClusterManager : IClusterManager
    {
        private IpcServiceClient<IClusterManager> _client;

        public IpcClusterManager(IpcServiceClient<IClusterManager> client)
        {
            this._client = client;
        }

        public Cluster CreateCluster(string name) =>
            this._client.InvokeAsync(manager => manager.CreateCluster(name)).Result;

        public Cluster GetCluster(string identifier) =>
            this._client.InvokeAsync(manager => manager.GetCluster(identifier)).Result;

        public bool DeleteCluster(string identifier) =>
            this._client.InvokeAsync(manager => manager.DeleteCluster(identifier)).Result;
    }
}
