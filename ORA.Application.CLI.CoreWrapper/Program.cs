using System;
using ORA.API;
using ORA.Core;

namespace ORA.Application.CLI.CoreWrapper
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            OraCore.Initialize();
            try
            {
                ORA.Application.CLI.Program.Main(args);
            }
            catch (Exception e)
            {
                Ora.GetLogger().Error(e);
                Console.WriteLine("An Error has occured :(");
            }
        }
    }
}
