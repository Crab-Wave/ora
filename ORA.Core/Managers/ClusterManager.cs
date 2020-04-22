using System;
using System.Collections.Generic;
using System.Linq;
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

        public Cluster CreateCluster(string name, string userDisplayName)
        {
            HttpResponse response = Ora.GetHttpClient().Post("/clusters?name=" + name + "&username=" + userDisplayName,
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            Console.WriteLine(response.Code);
            Console.WriteLine(response.Body);
            int code = response.Code;
            if (code == 200)
            {
                string identifier = JObject.Parse(response.Body)["id"].Value<string>();
                Cluster cluster = new Cluster(name, identifier);
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

            HttpResponse response = Ora.GetHttpClient().Get("/clusters/" + identifier);
            int code = response.Code;
            if (code == 200)
            {
                string name = JObject.Parse(response.Body)["name"].Value<string>();
                Cluster cluster = new Cluster(name, identifier);
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

            HttpResponse response = Ora.GetHttpClient().Delete("/clusters/" + identifier,
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return true;

            Exception exception = new Exception($"Couldn't find cluster with identifier {identifier}");
            Ora.GetLogger().Error(exception);
            return false;
        }

        public List<Member> GetMembers(string cluster)
        {
            HttpResponse response = Ora.GetHttpClient().Get("/clusters/" + cluster,
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return JObject.Parse(response.Body)["members"].Value<Member[]>().ToList();
            Exception exception = new Exception($"Couldn't find member in this cluster");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public Member GetMember(string cluster, string member)
        {
            HttpResponse
                response = Ora.GetHttpClient().Get("/clusters/" + cluster + "/members/" + member,
                    new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return JObject.Parse(response.Body).Value<Member>();

            Exception exception = new Exception($"Couldn't find member with identifier {member}");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public bool RemoveMember(string cluster, string member)
        {
            HttpResponse response = Ora.GetHttpClient().Delete(
                "/clusters/" + cluster + "/members/" + member,
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return true;
            Ora.GetLogger().Error($"Couldn't find member with identifier {member}");
            return false;
        }
    }
}
