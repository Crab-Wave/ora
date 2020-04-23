using System.Collections.Generic;
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

        public Cluster CreateCluster(string name, string userDisplayName) =>
            this._client.InvokeAsync(manager => manager.CreateCluster(name, userDisplayName)).Result;

        public Cluster GetCluster(string cluster) =>
            this._client.InvokeAsync(manager => manager.GetCluster(cluster)).Result;

        public bool DeleteCluster(string cluster) =>
            this._client.InvokeAsync(manager => manager.DeleteCluster(cluster)).Result;

        public List<Member> GetMembers(string cluster) =>
            this._client.InvokeAsync(manager => manager.GetMembers(cluster)).Result;

        public Member GetMember(string cluster, string member) =>
            this._client.InvokeAsync(manager => manager.GetMember(cluster, member)).Result;

        public bool RemoveMember(string cluster, string member) =>
            this._client.InvokeAsync(manager => manager.RemoveMember(cluster, member)).Result;
    }
}
