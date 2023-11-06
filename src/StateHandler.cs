using RimWorld;
using Verse;

namespace RimRPC
{
    public class StateHandler
    {
        private static string _colonyname;
        private static string _biome;
        private static int _years;
        private static int _days;
        private static int _dayhour;
        private static float _colonistnumber;
        private static Quadrum _quadrum;

        private static string GetColonyName() => Faction.OfPlayer.Name;

        public static void MenuState()
        {
            RimRPC.Presence.Details = "RPC_MainMenu".Translate();
            RimRPC.Presence.State = null;
            RimRPC.Presence.SmallImageKey = null;
            RimRPC.Presence.SmallImageText = null;
            DiscordRPC.UpdatePresence(ref RimRPC.Presence);
        }

        private static string BuildString(string which, string[] args)
        {
            var state = "";

            if (which == "state")
                return States.GetState(state, args);

            if (which == "details")
                return Details.GetDetails(args);
            
            return null;
        }

        public static void PushState(Map map)
        {
            var world = Current.Game?.World;
            
            if (world == null)
                RimRPC.Presence.Details = "RPC_MainMenu".Translate();
            
            else
            {
                var latitude = (map == null) ? 0f : Find.WorldGrid.LongLatOf(map.Tile).y;
                var longitude = (map == null) ? 0f : Find.WorldGrid.LongLatOf(map.Tile).x;

                _colonyname = GetColonyName();
                _years = _days / 60;
                _colonistnumber = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists.Count;
                _days = GenDate.DaysPassed;
                _dayhour = GenDate.HourOfDay(Find.TickManager.TicksAbs, longitude);
                _quadrum = GenDate.Quadrum(Find.TickManager.TicksAbs, longitude);
                _biome = "";

                if (map != null)
                {
                    var biome = Find.WorldGrid[map.uniqueID].biome;
                    _biome = biome.LabelCap;
                }

                string[] stateArgs = { _years.ToString(), _days.ToString(), _dayhour.ToString(), _quadrum.Label(), _biome };
                string[] detailsArgs = { _colonyname, _colonistnumber.ToString()};

                RimRPC.Presence.State = BuildString("state", stateArgs);
                RimRPC.Presence.Details = BuildString("details", detailsArgs);
                RimRPC.Presence.LargeImageText = "RimWorld";
                RimRPC.Presence.SmallImageKey = "inmap";
                RimRPC.Presence.SmallImageText = "RPC_Playing".Translate();

                if (RWRPCMod.Settings.RpcTime)
                    RimRPC.Presence.StartTimestamp = RimRPC.Started;
            }

            DiscordRPC.UpdatePresence(ref RimRPC.Presence);
        }
    }
}