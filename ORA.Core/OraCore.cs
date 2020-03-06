using System;
using ORA.API;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.Core.Encryption;
using ORA.Core.Http;
using ORA.Core.Loggers;

namespace ORA.Core
{
    public class OraCore : Ora
    {
        private readonly ICipher _cipher;

        private readonly HttpClient _httpClient;

        private readonly ILogger _logger;

        private OraCore()
        {
            this._logger = new SimpleLogger("OraCore");
            this._cipher = new RsaCipher(4096);
            this._httpClient = new UnirestHttpClient();
        }

        public static void Initialize() => SetInstance(new OraCore());

        public override ILogger Logger() => this._logger;

        public override HttpClient HttpClient() => this._httpClient;

        public override ICipher Cipher() => this._cipher;

        public override IClusterManager ClusterManager() =>
            throw new NotImplementedException("ClusterManager not implemented");

        public override INodeManager NodeManager() =>
            throw new NotImplementedException("NodeManager not implemented");
    }
}
