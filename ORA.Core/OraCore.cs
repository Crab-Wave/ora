using System;
using System.IO;
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
using File = ORA.API.File;

namespace ORA.Core
{
    public class OraCore : Ora
    {
        private readonly string _programDirectory;

        private readonly ILogger _logger;

        private readonly ICipher _cipher;

        private readonly ICompressor _compressor;

        private readonly IHttpClient _httpClient;

        private readonly IAuthManager _authManager;

        private readonly IIdentityManager _identityManager;

        private readonly IClusterManager _clusterManager;

        private readonly NetworkManager _networkManager;

        private readonly IFileManager _fileManager;

        private readonly INodeManager _nodeManager;

        private OraCore()
        {
            this._programDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                     Path.DirectorySeparatorChar + "ora";
            if (!System.IO.Directory.Exists(this._programDirectory))
                System.IO.Directory.CreateDirectory(this._programDirectory);
            this._logger = new SimpleLogger("OraCore");
            this._cipher = new RsaCipher();
            this._compressor = new ZipLibCompressor();
            this._httpClient = new UnirestHttpClient();
            string trackerPath = Path.Combine(this._programDirectory, "ora-tracker");
            if (System.IO.File.Exists(trackerPath))
            {
                string text = System.IO.File.ReadAllText(trackerPath).Trim();
                Uri uriResult;
                bool result = Uri.TryCreate(text, UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (result && this.IsOraTracker(text))
                    this._httpClient.SetBaseUrl(text);
                else
                    this._httpClient.SetBaseUrl("https://tracker.ora.crabwave.com");
            }
            else
            {
                System.IO.File.Create(trackerPath).Close();
                System.IO.File.WriteAllText(trackerPath, "https://tracker.ora.crabwave.com");
                this._httpClient.SetBaseUrl("https://tracker.ora.crabwave.com");
            }

            this._authManager = new AuthManager();
            this._identityManager = new IdentityManager(this._programDirectory);
            this._clusterManager = new ClusterManager();
            this._networkManager = new NetworkManager();
            this._networkManager.StartListening();
            this._fileManager = new FileManager(this._programDirectory);
            this._nodeManager = new NodeManager();
        }

        public static void Initialize() => SetInstance(new OraCore());

        public override string ProgramDirectory() => this._programDirectory;

        public override string Directory(params string[] path)
        {
            string[] paths = new string[path.Length + 1];
            paths[0] = this._programDirectory;
            for (int i = 0; i < path.Length; i++)
                paths[i + 1] = path[i];
            return Path.Combine(paths);
        }

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

        public override bool IsOraTracker(string url)
        {
            string random = new Random().Next().ToString();
            HttpResponse response = new UnirestHttpClient().Post(url + "/ping", new HttpRequest(random));
            if (response.Code == 200)
                return response.Body == random;
            return false;
        }
    }
}
