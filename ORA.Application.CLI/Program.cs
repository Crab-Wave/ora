using System;
using System.IO;
using CommandDotNet;
using CommandDotNet.FluentValidation;
using CommandDotNet.Help;
using ORA.API;
using File = System.IO.File;

namespace ORA.Application.CLI
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            AppRunner appRunner = new AppRunner<OraApplication>().UseFluentValidation();
            appRunner.AppSettings.Help.UsageAppName = "ora";

            return appRunner.Run(args);
        }
    }
}
