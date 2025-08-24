using Exiled.API.Features;
using Exiled.Events;
using Exiled.Events.EventArgs;
using Exiled.Events.EventArgs.Server;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundTimePlugin
{
    public class RoundTimePlugin : Plugin<Config>
    {
        public override string Name => "RoundTime";
        public override string Author => "VredLess";
        public override Version Version => new Version(1, 2, 1);

        internal static DateTime StartTime;
        internal static string roundstatus = "none";
        private CoroutineHandle nameUpdater;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;
            nameUpdater = Timing.RunCoroutine(UpdateName());
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnded;
            Timing.KillCoroutines(nameUpdater);
            base.OnDisabled();
        }
        private void OnRoundStarted()
        {
            StartTime = DateTime.Now;
        }
        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            roundstatus = "<color=#1750ad>Раунд окончен</color>";
            StartTime = DateTime.Now;
        }
        private IEnumerator<float> UpdateName()
        {
            while (true)
            {
                var elapsed = DateTime.Now - StartTime;
                // Format: BaseName [MM:SS]
                if (Round.InProgress)
                {
                    Server.Name = $"{Config.BaseServerName} <size=20>[ {roundstatus}: {elapsed:mm\\:ss} ]</size>";
                }
                else
                {
                    Server.Name = $"{Config.BaseServerName} <size=20>[ <color=#53de3e>Ожидаем игроков</color> ]</size>";
                }
                yield return Timing.WaitForSeconds(Config.UpTimeAmount);
            }
        }
    }
}
