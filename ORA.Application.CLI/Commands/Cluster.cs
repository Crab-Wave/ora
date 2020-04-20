using System;
using System.Collections.Generic;
using ORA.API;
using ORA.API.Managers;

namespace ORA.Application.CLI.Commands
{
    public class Cluster
    {
        public static void Exec(string command, string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("Missing arguments");

            if (args.Length > 1)
                throw new ArgumentException("To much arguments");

            switch (command)
            {
                case "create":
                    Ora.GetClusterManager().CreateCluster(args[0]);
                    break;
                case "rm":
                    Ora.GetClusterManager().DeleteCluster(args[0]);
                    break;
                case "info":
                    Ora.GetClusterManager().GetCluster(args[0]);
                    break;
                default:
                    ArgumentException exception = new ArgumentException("Command unknown");
                    throw exception;
            }
        }
    }
}
