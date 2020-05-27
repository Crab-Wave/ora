using System;
using ORA.API;
using ORA.API.Http;
using ORA.API.Managers;

namespace ORA.Core.Managers
{
    public class NodeManager : INodeManager
    {
        private string ip;

        public void Initialize()
        {
            HttpResponse response = Ora.GetHttpClient().Get("/ip");
            if (response.Code != 200)
            {
                Exception exception = new Exception("Couldn't get current IP");
                Ora.GetLogger().Error(exception);
                throw exception;
            }

            this.ip = response.Body;

            foreach (File ownedFile in Ora.GetFileManager().GetOwnedFiles())
            {
                Ora.GetHttpClient().Post($"/clusters/{ownedFile.Cluster}/files/owned",
                    new HttpRequest($"[\"{ownedFile.Hash}\"]").Set("Authorization",
                        "Bearer " + Ora.GetAuthManager().GetToken()));
            }
        }

        public string GetIp() => this.ip;

        public void RefreshIp()
        {
            HttpResponse response = Ora.GetHttpClient().Post("/refreship",
                new HttpRequest(this.GetIp()).Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
            if (response.Code != 200)
            {
                Exception exception = new Exception("Couldn't refresh IP");
                Ora.GetLogger().Error(exception);
                throw exception;
            }

            this.ip = response.Body;
        }
    }
}
