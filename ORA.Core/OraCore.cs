using System;
using ORA.API;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.Core.Http;
using ORA.Core.Loggers;

namespace ORA.Core
{
    public class OraCore : Ora
    {
        public static void Initialize() => SetInstance(new OraCore());

        private readonly ILogger _logger;
        private readonly IHttpClient _httpClient;

        private OraCore()
        {
            this._logger = new SimpleLogger("OraCore");
            this._httpClient = new UnirestHttpClient();
        }

        public override ILogger Logger() => this._logger;

        public override IHttpClient HttpClient() => this._httpClient;

        public override IClusterManager ClusterManager() =>
            throw new NotImplementedException("ClusterManager not implemented");

        public override INodeManager NodeManager() =>
            throw new NotImplementedException("ClusterManager not implemented");
    }
}
