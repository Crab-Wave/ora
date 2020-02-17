using System;
using System.Collections.Generic;
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
                response = Ora.GetHttpClient().Post("", new HttpRequest().Set("name", name)); //a modif avec léo
            int code = response.Code;
            if (code == 200)
            {
                string identifier = response.Body; //a modif avec léo
                foreach (var clus in this._clusters)
                {
                    if (clus.Key == identifier)
                    {
                        throw new ArgumentException("Cluster already exists ");
                    }
                }

                Cluster cluster = new OraCluster(name, identifier);
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
                response = Ora.GetHttpClient().Get("", new HttpRequest().Set("identifier", identifier));
            int code = response.Code;
            if (code == 200)
                return this.CreateCluster(identifier);
            Exception exception = new Exception($"Couldn't find cluster with this {identifier}");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public bool DeleteCluster(string identifier)
        {
            string name = "";
            this._clusters.Remove(identifier);
            HttpResponse
                response = Ora.GetHttpClient().Delete("", new HttpRequest().Set("identifier", identifier));

            Exception exception = new Exception($"Couldn't find cluster with this {identifier}");
            Ora.GetLogger().Error(exception);
            return false;
        }
    }
}
