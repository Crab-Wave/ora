using System;
using CommandDotNet;
using CommandDotNet.Help;
using ORA.API;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI
{
    [Command(Description = "cluster management", Name = "cluster")]
    public class Clusters
    {
        [Command(Description = "create a cluster", Name = "create")]
        public void Create([Operand(Description = "")] ClusterNameModel name)
        {
            Cluster cluster = Ora.GetClusterManager().CreateCluster(name.Name);
            Console.WriteLine($"Cluster {cluster.Name} created with identifier {cluster.Identifier}");
        }

        [Command(Description = "delete a cluster", Name = "delete")]
        public void Delete([Operand(Description = "")] ClusterIdentifierModel identifier)
        {
            bool sucess = Ora.GetClusterManager().DeleteCluster(identifier.Identifier);
            if (!sucess)
                Console.WriteLine($"Could not delete cluster with identifier {identifier.Identifier}");
            else
                Console.WriteLine($"Sucessfully deleted cluster with identifier {identifier.Identifier}");
        }
    }
}
