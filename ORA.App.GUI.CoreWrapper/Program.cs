using System;
using ORA.API;
using ORA.Core;
using ORA.Core.IPC;

namespace ORA.App.GUI.CoreWrapper
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            OraCoreIpc.Initialize();
            try
            {
                ORA.App.GUI.Program.Main(args);
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
