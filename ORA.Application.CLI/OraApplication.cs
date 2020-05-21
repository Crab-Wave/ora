using System;
using System.IO;
using System.Net;
using CommandDotNet;
using ORA.API;
using ORA.Application.CLI.Commands;

namespace ORA.Application.CLI
{
    [Command(Name = "ora", Description = "ORA base command")]
    public class OraApplication
    {
        [SubCommand]
        public Clusters Clusters
        {
            get;
            set;
        }

        [SubCommand]
        public Identities Identities
        {
            get;
            set;
        }

        [SubCommand]
        public Url Url
        {
            get;
            set;
        }
    }
}
