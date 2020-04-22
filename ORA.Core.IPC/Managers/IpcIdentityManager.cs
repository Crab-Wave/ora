using JKang.IpcServiceFramework;
using ORA.API;
using ORA.API.Managers;

namespace ORA.Core.IPC.Managers
{
    public class IpcIdentityManager : IIdentityManager
    {
        private IpcServiceClient<IIdentityManager> _client;

        public IpcIdentityManager(IpcServiceClient<IIdentityManager> client)
        {
            this._client = client;
        }

        public Identity GetIdentity() => this._client.InvokeAsync(manager => manager.GetIdentity()).Result;

        public Identity GenerateIdentity(string name) =>
            this._client.InvokeAsync(manager => manager.GenerateIdentity(name)).Result;
    }
}
