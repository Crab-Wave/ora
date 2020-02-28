using System;
using ORA.API;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.Core.Http;
using ORA.Core.Loggers;
using ORA.Core.Managers;

namespace ORA.Core
{
    public class OraCore : Ora
    {
        public static void Initialize() => SetInstance(new OraCore());

        private readonly ILogger _logger;
        private readonly IHttpClient _httpClient;
        private readonly IClusterManager _clusterManager;

        private OraCore()
        {
            this._logger = new SimpleLogger("OraCore");
            this._httpClient = new UnirestHttpClient();
            this._clusterManager = new ClusterManager();
        }

        public override ILogger Logger() => this._logger;

        public override IHttpClient HttpClient() => this._httpClient;
        public override ICipher Cipher() => throw new NotImplementedException();

        public override IClusterManager ClusterManager() => this._clusterManager;

        public override INodeManager NodeManager() =>
            throw new NotImplementedException("NodeManager not implemented");
    }
}
