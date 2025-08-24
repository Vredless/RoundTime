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
        public override Version Version => new Version(1, 2, 0);

        internal static DateTime StartTime;
        internal static string roundstatus = "none";
        private CoroutineHandle nameUpdater;
        private bool isroundstartedbool = false;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;
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
            isroundstartedbool = true;
            StartTime = DateTime.Now;
            nameUpdater = Timing.RunCoroutine(UpdateName());
        }
        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            isroundstartedbool = false;
            Timing.KillCoroutines(nameUpdater);
        }
        private IEnumerator<float> UpdateName()
        {
            while (true)
            {
                var elapsed = DateTime.Now - StartTime;
                // Format: BaseName [MM:SS]
                if (isroundstartedbool == true)
                {
                    Server.Name = $"{Config.BaseServerName} <size=20>[ {roundstatus}: {elapsed:mm\\:ss} ]</size>";
                }
                else
                {
                    Server.Name = $"{Config.BaseServerName} <size=20><color=#53de3e>[ Ожидаем игроков ]</color></size>";
                }
                yield return Timing.WaitForSeconds(Config.UpTimeAmount);
            }
        }
    }
}
