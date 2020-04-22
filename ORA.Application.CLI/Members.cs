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
        [Command(Description = "Get the members of the current cluster", Name = "get", Usage = "get <cluster>")]
        public void GetMembers([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel identifier)
        {
            Console.WriteLine($"Cluster: {identifier.ClusterIdentifier}");
            Console.WriteLine(
                $"Members: {String.Join(",\n       - ", Ora.Get().ClusterManager().GetMembers(identifier.ClusterIdentifier))}");
        }

        [Command(Description = "Remove a member", Name = "remove", Usage = "remove <cluster> <member>")]
        public bool Remove([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel cluster, [Operand(Description = "member identifier")]
            MemberIdentifierModel member)
        {
            if (!Ora.GetClusterManager().RemoveMember(cluster.ClusterIdentifier, member.MemberIdentifier))
            {
                Console.WriteLine($"Could not remove member with identifier {member.MemberIdentifier}");
                return true;
            }

            Console.WriteLine($"Successfully remove member with identifier {member.MemberIdentifier}");
            return false;
        }
    }
}
