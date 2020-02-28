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
        public static void Initialize() => SetInstance(new OraCore());

        private readonly ILogger _logger;
        private readonly ICipher _cipher;

        private OraCore()
        {
            this._logger = new SimpleLogger("OraCore");
            this._cipher = new RsaCipher(4096);
        }

        public override ILogger Logger() => this._logger;

        public override HttpClient NewHttpClient() => new UnirestHttpClient();

        public override HttpClient NewHttpClient(string baseUrl) => new UnirestHttpClient(baseUrl);

        public override ICipher Cipher() => this._cipher;

        public override IClusterManager ClusterManager() =>
            throw new NotImplementedException("ClusterManager not implemented");

        public override INodeManager NodeManager() =>
            throw new NotImplementedException("NodeManager not implemented");
    }
}
