using ORA.Core;

namespace ORA.Application.CLI.CoreWrapper
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            OraCore.Initialize();
            ORA.Application.CLI.Program.Main(args);
        }
    }
}
