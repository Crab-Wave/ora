using System;
using System.IO;
using JKang.IpcServiceFramework;
using ORA.API;
using ORA.API.Compression;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.API.Utils;
using ORA.Core.IPC.Compression;
using ORA.Core.IPC.Encryption;
using ORA.Core.IPC.Http;
using ORA.Core.IPC.Loggers;
using ORA.Core.IPC.Managers;

namespace ORA.Core.IPC
{
    public class OraCoreIpc : Ora
    {
        private readonly IpcServiceClient<StringProvider> _programDirectory;
        private readonly IpcServiceClient<StringProviderWithStringArray> _directory;
        private readonly ILogger _logger;
        private readonly ICipher _cipher;
        private readonly ICompressor _compressor;
        private readonly IHttpClient _httpClient;
        private readonly IAuthManager _authManager;
        private readonly IIdentityManager _identityManager;
        private readonly IClusterManager _clusterManager;
        private readonly INetworkManager _networkManager;
        private readonly IFileManager _fileManager;
        private readonly INodeManager _nodeManager;

        private OraCoreIpc()
        {
            this._programDirectory = new IpcServiceClientBuilder<StringProvider>().UseNamedPipe("ora-program-directory")
                .Build();
            this._directory = new IpcServiceClientBuilder<StringProviderWithStringArray>().UseNamedPipe("ora-directory")
                .Build();
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
            this._networkManager =
                new IpcNetworkManager(
                    new IpcServiceClientBuilder<INetworkManager>().UseNamedPipe("ora-network").Build());
            this._fileManager =
                new IpcFileManager(
                    new IpcServiceClientBuilder<IFileManager>().UseNamedPipe("ora-file").Build());
            this._nodeManager =
                new IpcNodeManager(new IpcServiceClientBuilder<INodeManager>().UseNamedPipe("ora-node").Build());
        }

        public static void Initialize() => SetInstance(new OraCoreIpc());

        public override string ProgramDirectory() =>
            this._programDirectory.InvokeAsync(provider => provider.Provide()).Result;

        public override string Directory(params string[] path) =>
            this._directory.InvokeAsync(provider => provider.Provide(path)).Result;

        public override ILogger Logger() => this._logger;

        public override IHttpClient HttpClient() => this._httpClient;

        public override ICipher Cipher() => this._cipher;

        public override IIdentityManager IdentityManager() => this._identityManager;

        public override IClusterManager ClusterManager() => this._clusterManager;

        public override ICompressor Compressor() => this._compressor;

        public override IAuthManager AuthManager() => this._authManager;

        public override INetworkManager NetworkManager() => this._networkManager;

        public override IFileManager FileManager() => this._fileManager;

        public override INodeManager NodeManager() => this._nodeManager;
    }
}
