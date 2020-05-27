using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ORA.API;
using ORA.API.Http;
using ORA.API.Managers;
using ORA.API.Network;
using ORA.API.Network.Packets;
using File = ORA.API.File;

namespace ORA.Core.Managers
{
    public class FileManager : IFileManager
    {
        private HashAlgorithm _hashAlgorithm;
        private Dictionary<string, Dictionary<string, File>> _files;

        public FileManager(string path)
        {
            this._hashAlgorithm = HashAlgorithm.Create("SHA1");
            this._files = new Dictionary<string, Dictionary<string, File>>();

            path = Path.Combine(path, "files");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            this.Discover(path, file =>
            {
                if (!this._files.ContainsKey(file.Cluster))
                    this._files.Add(file.Cluster, new Dictionary<string, File>());
                this._files[file.Cluster].Add(file.Hash, file);
            });
        }

        public File[] GetOwnedFiles() => this._files.Values.SelectMany(files => files.Values).ToArray();

        public File CreateFile(Cluster cluster, string realPath, string clusterPath)
        {
            if (!System.IO.File.Exists(realPath))
                throw new ArgumentException("File doesn't exists");
            byte[] data = System.IO.File.ReadAllBytes(realPath);
            string hash = this.HashToString(this._hashAlgorithm.ComputeHash(data));
            if (this.GetFile(cluster, hash, false) != null)
                throw new ArgumentException("File already exists");
            File file = new File(cluster.Identifier, clusterPath, hash, data.Length);

            HttpResponse response = Ora.GetHttpClient()
                .Post($"/clusters/{cluster.Identifier}/files",
                    new HttpRequest(file.ToString()).Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            if (response.Code != 200)
            {
                Exception exception =
                    new Exception($"Couldn't create file {clusterPath} in cluster {cluster.Identifier}");
                Ora.GetLogger().Error(exception);
                throw exception;
            }

            this.WriteFile(file, data);

            Ora.GetLogger().Info($"Created file {file.Path} with hash {file.Hash} in cluster {cluster.Identifier}");

            return file;
        }

        public bool RemoveFile(Cluster cluster, string hash)
        {
            HttpResponse response = Ora.GetHttpClient().Delete($"/clusters/{cluster.Identifier}/files?hash={hash}",
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));

            if (response.Code != 200)
            {
                Exception exception = new Exception($"Couldn't remove file {hash} from cluster {cluster.Identifier}");
                Ora.GetLogger().Error(exception);
                return false;
            }

            if (this._files.ContainsKey(cluster.Identifier))
            {
                string clusterPath = Ora.GetDirectory("files", cluster.Identifier);
                string filePath = Path.Combine(clusterPath, hash);
                this._files[cluster.Identifier].Remove(hash);
                System.IO.File.Delete(filePath);
                System.IO.File.Delete(filePath + ".info");
                if (this._files[cluster.Identifier].Count == 0)
                {
                    this._files.Remove(cluster.Identifier);
                    Directory.Delete(clusterPath);
                }
            }

            Ora.GetLogger().Info($"Removed file {hash} from cluster {cluster.Identifier}");

            return true;
        }

        public string[] GetFiles(Cluster cluster)
        {
            HttpResponse response = Ora.GetHttpClient().Get($"/clusters/{cluster.Identifier}",
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return JObject.Parse(response.Body)["files"].Children().Select(token => token.Value<string>())
                    .ToArray();

            Exception exception = new Exception($"Couldn't get all files");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public File GetFile(Cluster cluster, string hash) => this.GetFile(cluster, hash, true);

        public File GetFile(Cluster cluster, string hash, bool shouldError)
        {
            if (!this._files.ContainsKey(cluster.Identifier))
                this._files.Add(cluster.Identifier, new Dictionary<string, File>());
            if (this._files[cluster.Identifier].ContainsKey(hash))
                return this._files[cluster.Identifier][hash];

            if (!shouldError)
                return null;

            HttpResponse response = Ora.GetHttpClient().Get($"/clusters/{cluster.Identifier}/files?hash={hash}",
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));

            if (response.Code != 200)
            {
                Exception exception = new Exception($"Couldn't get file {hash} in cluster {cluster.Identifier}");
                Ora.GetLogger().Error(exception);
                throw exception;
            }

            JObject json = JObject.Parse(response.Body);

            string owner = null;
            foreach (JToken jToken in json["owners"].Children())
            {
                if (!jToken["id"].Value<string>().Equals(Ora.GetIdentityManager().GetIdentity().GetIdentifier()))
                {
                    owner = jToken["ip"].Value<string>();
                    break;
                }
            }

            if (String.IsNullOrEmpty(owner))
            {
                Exception exception = new Exception($"Couldn't get file {hash} in cluster {cluster.Identifier}");
                Ora.GetLogger().Error(exception);
                throw exception;
            }

            FilePacket filePacket = (FilePacket) Ora.GetNetworkManager()
                .SendPacket(owner, 5000, new RequestFilePacket(cluster.Identifier, hash));
            File file = new File(cluster.Identifier, json["path"].Value<string>(), hash, json["size"].Value<long>());

            this.WriteFile(file, filePacket.Content);

            Ora.GetHttpClient().Post($"/clusters/{cluster}/files/owned",
                new HttpRequest($"[\"{file.Hash}\"]").Set("Authorization",
                    "Bearer " + Ora.GetAuthManager().GetToken()));

            return file;
        }

        public byte[] GetFileContent(Cluster cluster, File file) =>
            System.IO.File.ReadAllBytes(Ora.GetDirectory("files", cluster.Identifier, file.Hash));

        public byte[] GetFileContent(Cluster cluster, string hash) =>
            System.IO.File.ReadAllBytes(Ora.GetDirectory("files", cluster.Identifier, hash));

        private string HashToString(byte[] hash) => String.Concat(hash.Select(b => b.ToString("x2")));

        private void WriteFile(File file, byte[] content)
        {
            string oraPath = Ora.GetDirectory("files", file.Cluster);
            if (!Directory.Exists(oraPath))
                Directory.CreateDirectory(oraPath);
            oraPath = Path.Combine(oraPath, file.Hash);
            System.IO.File.Create(oraPath).Close();
            System.IO.File.WriteAllBytes(oraPath, content);
            System.IO.File.Create(oraPath + ".info").Close();
            System.IO.File.WriteAllText(oraPath + ".info", file.Path + Environment.NewLine + file.Size);

            if (!this._files.ContainsKey(file.Cluster))
                this._files.Add(file.Cluster, new Dictionary<string, File>());
            this._files[file.Cluster].Add(file.Hash, file);
        }

        private void Discover(string path, Action<File> action)
        {
            foreach (string file in Directory.EnumerateFiles(path, "*.info"))
            {
                string fileName = Path.GetFileName(file);
                string[] content = System.IO.File.ReadAllText(file).Split(Environment.NewLine);
                string hash = fileName.Substring(0, fileName.Length - 5);
                string oraPath = content[0];
                long length = Int64.Parse(content[1]);
                action(new File(Path.GetFileName(path), oraPath, hash, length));
            }

            foreach (string directory in Directory.EnumerateDirectories(path))
                this.Discover(directory, action);
        }
    }
}
