using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using ORA.API;
using ORA.API.Http;
using ORA.API.Managers;

namespace ORA.Core
{
    public class OraCluster : Cluster
    {
        public OraCluster(string name, string identifier) : base(name, identifier)
        {
        }

        public override List<Member> GetMembers()
        {
            HttpResponse
                response = Ora.GetHttpClient().Get("/clusters/" + this.Identifier);
            int code = response.Code;
            if (code == 200)
                return JObject.Parse(response.Body)["members"].Value<Member[]>().ToList();
            Exception exception = new Exception($"Couldn't find member in this cluster");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public override Member GetMember(string identifier)
        {
            HttpResponse
                response = Ora.GetHttpClient().Get("/clusters/" + this.Identifier + "/members/" + identifier);
            int code = response.Code;
            if (code == 200)
                return JObject.Parse(response.Body).Value<Member>();

            Exception exception = new Exception($"Couldn't find member with identifier {identifier}");
            Ora.GetLogger().Error(exception);
            throw exception;
        }

        public override bool RemoveMember(string identifier)
        {
            HttpResponse
                response = Ora.GetHttpClient().Delete("/clusters/" + this.Identifier + "/members/" + identifier,
                    new HttpRequest().Set("Authorization", "dummy"));
            int code = response.Code;
            if (code == 200)
                return true;
            Ora.GetLogger().Error($"Couldn't find member with identifier {identifier}");
            return false;
        }
    }
}
