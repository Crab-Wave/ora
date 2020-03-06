using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ORA.API;
using ORA.API.Http;
using ORA.API.Managers;

namespace ORA.Core.Managers
{
    public class ClusterManager : IClusterManager
    {
        private Dictionary<string, Cluster> _clusters;

        public ClusterManager()
        {
            this._clusters = new Dictionary<string, Cluster>();
        }

        public Cluster CreateCluster(string name)
        {
            HttpResponse
                response = Ora.GetHttpClient()
                    .Post("/clusters?name=" + name, new HttpRequest().Set("Authorization", "dummy"));
            int code = response.Code;
            if (code == 200)
            {
                string identifier = JObject.Parse(response.Body)["id"].Value<string>();
                Cluster cluster = new OraCluster(name, identifier);
                this._clusters.Add(identifier, cluster);
                Ora.GetLogger().Info($"Created cluster {name} with identifier {identifier}");
                return cluster;
            }

            Exception exception = new Exception($"Couldn't create cluster {name}");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public Cluster GetCluster(string identifier)
        {
            if (this._clusters.ContainsKey(identifier))
                return this._clusters[identifier];

            HttpResponse
                response = Ora.GetHttpClient().Get("/clusters?id=" + identifier);
            int code = response.Code;
            if (code == 200)
            {
                string name = JObject.Parse(response.Body)["name"].Value<string>();
                Cluster cluster = new OraCluster(name, identifier);
                this._clusters.Add(identifier, cluster);
                return cluster;
            }

            Exception exception = new Exception($"Couldn't find cluster with identifier {identifier}");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public bool DeleteCluster(string identifier)
        {
            this._clusters.Remove(identifier);
            HttpResponse
                response = Ora.GetHttpClient().Delete("/clusters?id=" + identifier, new HttpRequest().Set("Authorization", "dummy"));
            int code = response.Code;
            if (code == 200)
                return true;
            Exception exception = new Exception($"Couldn't find cluster with identifier {identifier}");
            Ora.GetLogger().Error(exception);
            return false;
        }
    }
}
