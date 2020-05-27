using System;
using System.Text;
using ORA.API;
using ORA.API.Loggers;
using ORA.Core;
using ORA.Core.IPC;

namespace ORA.Application.CLI.CoreWrapper
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            OraCoreIpc.Initialize();
            try
            {
                ORA.Application.CLI.Program.Main(args);
            }
            catch (Exception e)
            {
                Ora.GetLogger().ShouldPrint(true);
                Ora.GetLogger().Error(e);
                Console.WriteLine("An Error has occured :(");
            }
        }
    }
}
