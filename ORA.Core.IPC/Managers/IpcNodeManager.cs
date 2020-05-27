using JKang.IpcServiceFramework;
using ORA.API;
using ORA.API.Managers;

namespace ORA.Core.IPC.Managers
{
    public class IpcNodeManager : INodeManager
    {
        private IpcServiceClient<INodeManager> _client;

        public IpcNodeManager(IpcServiceClient<INodeManager> client)
        {
            this._client = client;
        }

        public void Initialize() => this._client.InvokeAsync(manager => manager.Initialize()).Wait();

        public string GetIp() => this._client.InvokeAsync(manager => manager.GetIp()).Result;

        public void RefreshIp() => this._client.InvokeAsync(manager => manager.RefreshIp()).Wait();
    }
}
