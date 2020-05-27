using System;
using CommandDotNet;
using ORA.API;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI.Commands
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

        [SubCommand]
        public Admins Admins
        {
            get;
            set;
        }

        [SubCommand]
        public Files Files
        {
            get;
            set;
        }

        [Command(Description = "Create a Cluster", Name = "create", Usage = "cluster create <name> <username>")]
        public void Create([Operand(Name = "cluster", Description = "Cluster name")]
            ClusterNameModel name, [Operand(Name = "username", Description = "Username")]
            UserNameModel username)
        {
            Cluster cluster = Ora.GetClusterManager().CreateCluster(name.Name, username.UserName);
            Console.WriteLine($"Cluster {cluster.Name} created with identifier {cluster.Identifier}");
        }

        [Command(Description = "Delete a cluster", Name = "delete", Usage = "cluster delete <identifier>")]
        public void Delete([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel identifier)
        {
            bool success = Ora.GetClusterManager().DeleteCluster(identifier.ClusterIdentifier);
            if (!success)
                Console.WriteLine($"Could not delete cluster with identifier {identifier.ClusterIdentifier}");
            else
                Console.WriteLine($"Successfully deleted cluster with identifier {identifier.ClusterIdentifier}");
        }

        [Command(Description = "Join a cluster", Name = "join", Usage = "cluster join <cluster> <username>")]
        public void Join([Operand(Name = "cluster", Description = "Cluster identifier")]
            ClusterIdentifierModel identifier, [Operand(Name = "username", Description = "Username")]
            UserNameModel username)
        {
            bool success = Ora.GetClusterManager().JoinCluster(identifier.ClusterIdentifier, username.UserName);
            if (!success)
                Console.WriteLine($"Could not join cluster with identifier {identifier.ClusterIdentifier}");
            else
                Console.WriteLine($"Successfully joined cluster with identifier {identifier.ClusterIdentifier}");
        }
    }
}
