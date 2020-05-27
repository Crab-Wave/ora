using System;
using CommandDotNet;
using CommandDotNet.Help;
using ORA.API;
using ORA.Application.CLI.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using File = ORA.API.File;

namespace ORA.Application.CLI.Commands
{
    [Command(Description = "Files management command", Name = "files")]
    public class Files
    {
        [Command(Description = "Get the files of the cluster", Name = "get", Usage = "get <cluster>")]
        public void GetFiles([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel identifier)
        {
            Cluster cluster = Ora.GetClusterManager().GetCluster(identifier.ClusterIdentifier);
            Console.WriteLine($"Cluster: {identifier.ClusterIdentifier}");
            Console.WriteLine(
                $"Files: {String.Join(",\n       - ", Ora.GetFileManager().GetFiles(cluster).Select(s => this.FileToString(Ora.GetFileManager().GetFile(cluster, s))))}");
        }

        private string FileToString(File file) => $"{file.Path} / {file.Hash}";

        [Command(Description = "Remove a file", Name = "remove", Usage = "remove <cluster> <hash>")]
        public void Remove([Operand(Description = "Cluster identifier")]
            ClusterIdentifierModel identifier, [Operand(Description = "File Hash")] FileHashModel hash)
        {
            Cluster cluster = Ora.GetClusterManager().GetCluster(identifier.ClusterIdentifier);
            if (!Ora.GetFileManager().RemoveFile(cluster, hash.Hash))
                Console.WriteLine($"Could not remove file with hash {hash.Hash}");
            else
                Console.WriteLine($"Successfully removed file with hash {hash.Hash}");
        }

        [Command(Description = "Add a file", Name = "add", Usage = "add <cluster> <path> <name>")]
        public void Add([Operand(Name = "cluster", Description = "Cluster identifier")]
            ClusterIdentifierModel identifier, [Operand(Name = "path", Description = "Path")]
            FileInfo file, [Operand(Name = "name", Description = "Name")]
            UserNameModel name)
        {
            Cluster cluster = Ora.GetClusterManager().GetCluster(identifier.ClusterIdentifier);
            try
            {
                Ora.GetFileManager().CreateFile(cluster, file.FullName, name.UserName);
                Console.WriteLine($"Successfully added file");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not add file");
            }
        }

        [Command(Description = "Get a file content", Name = "content", Usage = "content <cluster> <hash>")]
        public void Get([Operand(Name = "cluster", Description = "Cluster identifier")]
            ClusterIdentifierModel identifier, [Operand(Name = "hash", Description = "Hash")]
            FileHashModel hash)
        {
            Cluster cluster = Ora.GetClusterManager().GetCluster(identifier.ClusterIdentifier);
            try
            {
                Console.OpenStandardOutput().Write(Ora.GetFileManager().GetFileContent(cluster, hash.Hash));
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't get file content");
            }
        }
    }
}
