using CommandDotNet;
using CommandDotNet.FluentValidation;

namespace ORA.Application.CLI
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return new AppRunner<OraApplication>().UseFluentValidation().Run(args);
        }
    }
}
