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
        [Command(Description = "Get the members of the cluster", Name = "get", Usage = "get <cluster>")]
        public void GetMembers([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel identifier)
        {
            Console.WriteLine($"Cluster: {identifier.ClusterIdentifier}");
            Console.WriteLine(
                $"Members: {String.Join(",\n       - ", Ora.Get().ClusterManager().GetMembers(identifier.ClusterIdentifier))}");
        }

        [Command(Description = "Remove a member", Name = "remove", Usage = "remove <cluster> <member>")]
        public void Remove([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel cluster, [Operand(Description = "Member identifier")]
            MemberIdentifierModel member)
        {
            if (!Ora.GetClusterManager().RemoveMember(cluster.ClusterIdentifier, member.MemberIdentifier))
                Console.WriteLine($"Could not remove member with identifier {member.MemberIdentifier}");
            else
                Console.WriteLine($"Successfully removed member with identifier {member.MemberIdentifier}");
        }

        [Command(Description = "Invite a user", Name = "invite", Usage = "invite <cluster> <user>")]
        public void Invite([Operand(Name = "cluster", Description = "Cluster identifier")]
            ClusterIdentifierModel cluster, [Operand(Name = "user", Description = "User identifier")]
            MemberIdentifierModel member)
        {
            if (!Ora.GetClusterManager().InviteMember(cluster.ClusterIdentifier, member.MemberIdentifier))
                Console.WriteLine($"Could not invite user with identifier {member.MemberIdentifier}");
            else
                Console.WriteLine($"Successfully invited user with identifier {member.MemberIdentifier}");
        }
    }
}
