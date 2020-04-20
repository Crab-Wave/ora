using System;
using ORA.API;
using ORA.Core;

namespace ORA.Application.GUI.CoreWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            OraCore.Initialize();
            try
            {
                ORA.Application.GUI.Program.Main(args);
            }
            catch (Exception e)
            {
                Ora.GetLogger().Error(e);
                Console.WriteLine("An Error has occured :(");
            }
        }
    }
}
