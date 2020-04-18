using System;
using ORA.API;
using ORA.Core;

namespace ORA.App.GUI.CoreWrapper
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            OraCore.Initialize();
            try
            {
                ORA.App.GUI.Program.Main(args);
            }
            catch (Exception e)
            {
                Ora.GetLogger().Error(e);
                Console.WriteLine("An Error has occured :(");
            }
        }
    }
}
