using JKang.IpcServiceFramework;
using ORA.API.Http;

namespace ORA.Core.IPC.Http
{
    public class IpcHttpClient : IHttpClient
    {
        private IpcServiceClient<IHttpClient> _client;

        public IpcHttpClient(IpcServiceClient<IHttpClient> client)
        {
            this._client = client;
        }

        public void SetBaseUrl(string baseUrl) =>
            this._client.InvokeAsync(client => client.SetBaseUrl(baseUrl)).Wait();

        public string GetBaseUrl() => this._client.InvokeAsync(client => client.GetBaseUrl()).Result;

        public HttpResponse Get(string path, HttpRequest request) =>
            this._client.InvokeAsync(client => client.Get(path, request)).Result;

        public HttpResponse Post(string path, HttpRequest request) =>
            this._client.InvokeAsync(client => client.Post(path, request)).Result;

        public HttpResponse Delete(string path, HttpRequest request) =>
            this._client.InvokeAsync(client => client.Delete(path, request)).Result;

        public HttpResponse Put(string path, HttpRequest request) =>
            this._client.InvokeAsync(client => client.Put(path, request)).Result;
    }
}
