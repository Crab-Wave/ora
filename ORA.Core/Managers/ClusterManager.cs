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
            int code = response.Code;
            if (code == 200)
            {
                string identifier = JObject.Parse(response.Body)["id"].Value<string>();
                Cluster cluster = new Cluster(name, identifier, Ora.GetIdentityManager().GetIdentity().GetIdentifier());
                this._clusters.Add(identifier, cluster);
                Ora.GetLogger().Info($"Created cluster {name} with identifier {identifier}");
                return cluster;
            }

            Exception exception = new Exception($"Couldn't create cluster {name}");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public List<Cluster> GetClusters()
        {
            HttpResponse response = Ora.GetHttpClient().Get("/clusters");
            int code = response.Code;
            if (code == 200)
            {
                List<Cluster> clusters = new List<Cluster>();
                foreach (var cluster in JArray.Parse(response.Body))
                {
                    Cluster c = new Cluster(cluster["name"].Value<string>(), cluster["id"].Value<string>(),
                        cluster["owner"].Value<string>());
                    if (!this._clusters.ContainsKey(c.Identifier))
                        this._clusters.Add(c.Identifier, c);
                    else
                        this._clusters[c.Identifier] = c;
                    clusters.Add(c);
                }

                return clusters;
            }

            Exception exception = new Exception($"Couldn't get all clusters");
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
                JToken json = JObject.Parse(response.Body);
                Cluster cluster = new Cluster(json["name"].Value<string>(), json["id"].Value<string>(),
                    json["owner"].Value<string>());
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
            HttpResponse response = Ora.GetHttpClient().Get("/clusters/" + cluster + "/members",
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return JObject.Parse(response.Body)["members"].Children().Select(token =>
                    new Member(token["id"].Value<string>(), token["name"].Value<string>())).ToList();
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

        public bool InviteMember(string cluster, string user)
        {
            HttpResponse response = Ora.GetHttpClient().Post(
                "/clusters/" + cluster + "/members?id=" + user,
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return true;
            Ora.GetLogger().Error($"Couldn't invite member with identifier {user}");
            return false;
        }

        public bool JoinCluster(string cluster, string displayName)
        {
            HttpResponse response = Ora.GetHttpClient().Post(
                "/clusters/" + cluster + "/join?username=" + displayName,
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return true;
            Ora.GetLogger().Error($"Couldn't join cluster with identifier {cluster}");
            return false;
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

        public bool AddAdmin(string cluster, string member)
        {
            HttpResponse response = Ora.GetHttpClient().Post(
                "/clusters/" + cluster + "/admins?id=" + member,
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return true;
            Ora.GetLogger().Error($"Couldn't add member with identifier {member} as an admin");
            return false;
        }

        public bool RemoveAdmin(string cluster, string member)
        {
            HttpResponse response = Ora.GetHttpClient().Delete(
                "/clusters/" + cluster + "/admins?id=" + member,
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return true;
            Ora.GetLogger().Error($"Couldn't remmove member with identifier {member} as an admin");
            return false;
        }

        public List<string> GetAdmins(string cluster)
        {
            HttpResponse response = Ora.GetHttpClient().Get("/clusters/" + cluster + "/admins",
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            int code = response.Code;
            if (code == 200)
                return JArray.Parse(response.Body).Children().Select(token => token.Value<string>()).ToList();
            Exception exception = new Exception($"Couldn't find member in this cluster");
            Ora.GetLogger().Error(exception);
            throw exception;
        }
    }
}
