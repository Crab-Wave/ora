using System;
using JKang.IpcServiceFramework;
using ORA.API;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.Core.IPC.Encryption;
using ORA.Core.IPC.Http;
using ORA.Core.IPC.Loggers;
using ORA.Core.IPC.Managers;

namespace ORA.Core.IPC
{
    public class OraCoreIpc : Ora
    {
        private readonly ICipher _cipher;
        private readonly IClusterManager _clusterManager;
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;

        public OraCoreIpc()
        {
            this._logger = new IpcLogger(new IpcServiceClientBuilder<ILogger>().UseNamedPipe("ora-logger").Build());
            this._httpClient =
                new IpcHttpClient(new IpcServiceClientBuilder<IHttpClient>().UseNamedPipe("ora-http").Build());
            this._cipher = new IpcCipher(new IpcServiceClientBuilder<ICipher>().UseNamedPipe("ora-cipher").Build());
            this._clusterManager =
                new IpcClusterManager(
                    new IpcServiceClientBuilder<IClusterManager>().UseNamedPipe("ora-cluster").Build());
        }

        public static void Initialize() => SetInstance(new OraCoreIpc());

        public override ILogger Logger() => this._logger;

        public override IHttpClient HttpClient() => this._httpClient;

        public override ICipher Cipher() => this._cipher;

        public override IClusterManager ClusterManager() => this._clusterManager;

        public override INodeManager NodeManager() =>
            throw new NotImplementedException("NodeManager not implemented");
    }
}
