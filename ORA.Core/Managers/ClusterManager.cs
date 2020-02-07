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
                Cluster cluster = new BasicCluster(name, identifier);
                Ora.GetLogger().Info($"Created cluster {name} with identifier {identifier}");
                return cluster;
            }

            Exception exception = new Exception($"Couldn't create cluster {name}");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public Cluster GetCluster(string identifier) => throw new System.NotImplementedException();

        public bool DeleteCluster(string identifier) => throw new System.NotImplementedException();
    }
}
