using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using Player = Exiled.API.Features.Player;

namespace RoundTimePlugin
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class RoundTimeCommand : ICommand
    {
        public string Command => "rtime";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Shows elapsed time since the round started (russian language).";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Round.InProgress)
            {
                response = "[RoundTime] Раунд ещё не начался.";
                return false;
            }

            var elapsed = DateTime.Now - RoundTimePlugin.StartTime;
            response = $"[RoundTime] {RoundTimePlugin.roundstatus}: {elapsed:mm\\:ss}";
            return true;
        }
    }
}