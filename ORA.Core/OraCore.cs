﻿using System;
using ORA.API;
using ORA.API.Compression;
using ORA.API.Encryption;
using ORA.API.Http;
using ORA.API.Loggers;
using ORA.API.Managers;
using ORA.Core.Compression;
using ORA.Core.Encryption;
using ORA.Core.Http;
using ORA.Core.Loggers;
using ORA.Core.Managers;

namespace ORA.Core
{
    public class OraCore : Ora
    {
        private readonly ILogger _logger;

        private readonly HttpClient _httpClient;

        private readonly ICipher _cipher;

        private readonly IClusterManager _clusterManager;

        private readonly ICompressor _compressor;

        private OraCore()
        {
            this._logger = new SimpleLogger("OraCore");
            this._httpClient = new UnirestHttpClient();
            this._httpClient.BaseUrl = "https://tracker.ora.crabwave.com";
            this._cipher = new RsaCipher(4096);
            this._clusterManager = new ClusterManager();
            this._compressor = new ZstdCompressor();
        }

        public static void Initialize() => SetInstance(new OraCore());

        public override ILogger Logger() => this._logger;

        public override HttpClient HttpClient() => this._httpClient;

        public override ICipher Cipher() => this._cipher;

        public override IClusterManager ClusterManager() => this._clusterManager;

        public override INodeManager NodeManager() =>
            throw new NotImplementedException("NodeManager not implemented");

        public override ICompressor Compressor() => this._compressor;
    }
}
