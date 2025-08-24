using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;

namespace RoundTimePlugin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetAbcCommand : ICommand
    {
        public string Command => "setevent";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Sets server status to event via roundtime plugin";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            
            response = "Статус сервера успешно изменён!";
            return true;
        }
    }
}
