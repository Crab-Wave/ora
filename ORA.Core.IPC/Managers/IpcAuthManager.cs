using JKang.IpcServiceFramework;
using ORA.API.Managers;

namespace ORA.Core.IPC.Managers
{
    public class IpcAuthManager : IAuthManager
    {
        private IpcServiceClient<IAuthManager> _client;

        public IpcAuthManager(IpcServiceClient<IAuthManager> client)
        {
            this._client = client;
        }

        public string Authenticate() => this._client.InvokeAsync(manager => manager.Authenticate()).Result;

        public string GetToken() => this._client.InvokeAsync(manager => manager.GetToken()).Result;

        public string RefreshToken() => this._client.InvokeAsync(manager => manager.RefreshToken()).Result;

        public bool IsAuthenticated() => this._client.InvokeAsync(manager => manager.IsAuthenticated()).Result;

        public void Disconnect() => this._client.InvokeAsync(manager => manager.Disconnect()).Wait();
    }
}
