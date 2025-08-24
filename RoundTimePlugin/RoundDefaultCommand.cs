using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;

namespace RoundTimePlugin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RoundDefaultCommand : ICommand
    {
        public string Command => "statusdefault";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Sets server status to default via roundtime plugin";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            RoundTimePlugin.roundstatus = "Раунд идёт";
            response = "Статус сервера успешно изменён!";
            return true;
        }
    }
}
