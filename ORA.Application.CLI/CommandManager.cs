using System.Collections.Generic;
using ORA.Application.CLI.Commands;

namespace ORA.Application.CLI
{
    public class CommandManager
    {
        private Dictionary<string, ManagementCommand> ManagementCommands;

        public CommandManager()
        {
            this.ManagementCommands = new Dictionary<string, ManagementCommand>();
        }
    }
}
