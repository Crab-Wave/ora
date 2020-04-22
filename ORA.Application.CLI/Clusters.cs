using System;
using CommandDotNet;
using CommandDotNet.Help;
using ORA.API;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI
{
    [Command(Description = "Cluster management command", Name = "cluster")]
    public class Clusters
    {
        [SubCommand]
        public Members Members
        {
            get;
            set;
        }

        [Command(Description = "Create a Cluster", Name = "create", Usage = "cluster create <name>")]
        public void Create([Operand(Description = "Cluster name")]
            ClusterNameModel name)
        {
            Cluster cluster = Ora.GetClusterManager().CreateCluster(name.Name);
            Console.WriteLine($"Cluster {cluster.Name} created with identifier {cluster.Identifier}");
        }

        [Command(Description = "Delete a cluster", Name = "delete", Usage = "cluster delete <identifier>")]
        public void Delete([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel identifier)
        {
            bool sucess = Ora.GetClusterManager().DeleteCluster(identifier.ClusterIdentifier);
            if (!sucess)
                Console.WriteLine($"Could not delete cluster with identifier {identifier.ClusterIdentifier}");
            else
                Console.WriteLine($"Successfully deleted cluster with identifier {identifier.ClusterIdentifier}");
        }
    }
}
