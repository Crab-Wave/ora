using System;
using CommandDotNet;
using CommandDotNet.Help;
using ORA.API;
using ORA.Application.CLI.Objects;
using System.Collections.Generic;

namespace ORA.Application.CLI
{
    [Command(Description = "Members management command", Name = "members")]
    public class Members
    {
        private Cluster _cluster = Ora.Get().ClusterManager().CreateCluster("cluster test");

        [Command(Description = "Get the members of the current cluster", Name = "get all", Usage = "get")]
        public void GetMembers()
        {
            List<Member> members = this._cluster.GetMembers();
            string phrase = "";
            for (int i = 0; i < members.Count - 1; i++)
                phrase += members[i].Name + ", ";

            phrase += members[members.Count - 1].Name;
            Console.WriteLine("those are the members of the current cluster : " + phrase);
        }

        [Command(Description = "Delete a cluster", Name = "delete", Usage = "cluster delete <identifier>")]
        public bool Delete(string identifier)
        {
            bool sucess = this._cluster.RemoveMember(identifier);
            if (!sucess)
            {
                Console.WriteLine($"Could not delete member with identifier {identifier}");
                return true;
            }

            Console.WriteLine($"Successfully deleted member with identifier {identifier}");
            return false;
        }
    }
}
