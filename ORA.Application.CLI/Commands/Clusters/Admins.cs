using System;
using CommandDotNet;
using CommandDotNet.Help;
using ORA.API;
using ORA.Application.CLI.Objects;
using System.Collections.Generic;

namespace ORA.Application.CLI.Commands
{
    [Command(Description = "Admins management command", Name = "admins")]
    public class Admins
    {
        [Command(Description = "Get the members of the cluster", Name = "get", Usage = "get <cluster>")]
        public void GetAdmins([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel identifier)
        {
            Console.WriteLine($"Cluster: {identifier.ClusterIdentifier}");
            Console.WriteLine(
                $"Admins: {String.Join(",\n       - ", Ora.Get().ClusterManager().GetAdmins(identifier.ClusterIdentifier))}");
        }

        [Command(Description = "Remove an admin", Name = "remove", Usage = "remove <cluster> <admin>")]
        public void Remove([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel cluster, [Operand(Description = "Admin identifier")]
            MemberIdentifierModel member)
        {
            if (!Ora.GetClusterManager().RemoveAdmin(cluster.ClusterIdentifier, member.MemberIdentifier))
                Console.WriteLine($"Could not remove admin with identifier {member.MemberIdentifier}");
            else
                Console.WriteLine($"Successfully removed admin with identifier {member.MemberIdentifier}");
        }

        [Command(Description = "Add an admin", Name = "add", Usage = "add <cluster> <user>")]
        public void Add([Operand(Name = "cluster", Description = "Cluster identifier")]
            ClusterIdentifierModel cluster, [Operand(Name = "user", Description = "User identifier")]
            MemberIdentifierModel member)
        {
            if (!Ora.GetClusterManager().AddAdmin(cluster.ClusterIdentifier, member.MemberIdentifier))
                Console.WriteLine($"Could add user with identifier {member.MemberIdentifier} as an admin");
            else
                Console.WriteLine($"Successfully added user with identifier {member.MemberIdentifier} as an admin");
        }
    }
}
