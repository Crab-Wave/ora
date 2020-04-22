using System;
using JKang.IpcServiceFramework;
using ORA.API;
using ORA.API.Compression;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.Core.IPC.Compression;
using ORA.Core.IPC.Encryption;
using ORA.Core.IPC.Http;
using ORA.Core.IPC.Loggers;
using ORA.Core.IPC.Managers;

namespace ORA.Core.IPC
{
    public class OraCoreIpc : Ora
    {
        private readonly ILogger _logger;
        private readonly ICipher _cipher;
        private readonly ICompressor _compressor;
        private readonly IHttpClient _httpClient;
        private readonly IAuthManager _authManager;
        private readonly IIdentityManager _identityManager;
        private readonly IClusterManager _clusterManager;

        public OraCoreIpc()
        {
            this._logger = new IpcLogger(new IpcServiceClientBuilder<ILogger>().UseNamedPipe("ora-logger").Build());
            this._cipher = new IpcCipher(new IpcServiceClientBuilder<ICipher>().UseNamedPipe("ora-cipher").Build());
            this._compressor =
                new IpcCompressor(new IpcServiceClientBuilder<ICompressor>().UseNamedPipe("ora-compressor").Build());
            this._httpClient =
                new IpcHttpClient(new IpcServiceClientBuilder<IHttpClient>().UseNamedPipe("ora-http").Build());
            this._authManager =
                new IpcAuthManager(new IpcServiceClientBuilder<IAuthManager>().UseNamedPipe("ora-auth").Build());
            this._identityManager = new IpcIdentityManager(new IpcServiceClientBuilder<IIdentityManager>()
                .UseNamedPipe("ora-identity").Build());
            this._clusterManager =
                new IpcClusterManager(
                    new IpcServiceClientBuilder<IClusterManager>().UseNamedPipe("ora-cluster").Build());
        }

        public static void Initialize() => SetInstance(new OraCoreIpc());

        public override ILogger Logger() => this._logger;

        public override IHttpClient HttpClient() => this._httpClient;

        public override ICipher Cipher() => this._cipher;

        public override IIdentityManager IdentityManager() => this._identityManager;

        public override IClusterManager ClusterManager() => this._clusterManager;

        public override INodeManager NodeManager() =>
            throw new NotImplementedException("NodeManager not implemented");

        public override ICompressor Compressor() => this._compressor;

        public override IAuthManager AuthManager() => this._authManager;
    }
}
