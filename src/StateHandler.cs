using RimWorld;
using System.Linq;
using Verse;

namespace RimRPC
{
    public class StateHandler
    {
        private static string _colonyname;
        private static int _years;
        private static int _days;
        private static int _dayhour;
        private static float _colonistnumber;
        private static Quadrum _quadrum;

        private static string GetColonyName()
        {
            return Faction.OfPlayer.Name;
        }

        public static int GetDaysSurvived()
        {
            long ticks = Find.TickManager.TicksAbs;
            int num = (int)(ticks / 60000L);
            
            if (num < 0L)
                ticks--;
            
            return num;
        }

        public static void MenuState()
        {
            RimRPC.Presence.Details = "RPC_MainMenu".Translate();
            RimRPC.Presence.State = null;
            RimRPC.Presence.SmallImageKey = null;
            RimRPC.Presence.SmallImageText = null;
            DiscordRPC.UpdatePresence(ref RimRPC.Presence);
        }

        private static string BuildString(string which)
        {
            var state = "";

            if (which == "state")
                return GetState(state);

            if (which == "details")
                return GetDetails();
            
            return null;
        }

        private static string GetState(string state)
        {
            if (RWRPCMod.Settings.RpcCustomBottom)
                return RWRPCMod.Settings.RpcCustomBottomText;
            
            if (RWRPCMod.Settings.RpcDay)
                state += $"{"RPC_Day".Translate()} {_days}";
            
            if (RWRPCMod.Settings.RpcDay && RWRPCMod.Settings.RpcHour)
                state += $" ({_dayhour}{"RPC_Hour".Translate()})";
            
            if (RWRPCMod.Settings.RpcQuadrum)
            {
                if (RWRPCMod.Settings.RpcDay)
                {
                    state += " | ";
                }
                state += _quadrum;
            }

            if (RWRPCMod.Settings.RpcYear)
            {
                if (RWRPCMod.Settings.RpcQuadrum || RWRPCMod.Settings.RpcDay)
                    state += " | ";
                
                if (RWRPCMod.Settings.RpcYearShort)
                {
                    state += $"{_years}{"RPC_ShortYears".Translate()}";
                    return state;
                }
                state += $"{"RPC_Years".Translate()} {_years}";
            }

            return state;
        }

        private static string GetDetails()
        {
            if (RWRPCMod.Settings.RpcCustomTop)
                return RWRPCMod.Settings.RpcCustomTopText;
            
            if (RWRPCMod.Settings.RpcColony)
            {
                if (RWRPCMod.Settings.RpcColonistCount)
                {
                    return _colonyname + " (" + _colonistnumber + ")";
                }
                return _colonyname;
            }
            if (RWRPCMod.Settings.RpcColonistCount)
            {
                return _colonistnumber + " RPC_Colonists".Translate();
            }

            return null;
        }

        public static void PushState(Map map)
        {
            var world = Current.Game?.World;
            
            if (world == null)
                RimRPC.Presence.Details = "RPC_MainMenu".Translate();
            
            else
            {
                float latitude = (map == null) ? 0f : Find.WorldGrid.LongLatOf(map.Tile).y;
                float longitude = (map == null) ? 0f : Find.WorldGrid.LongLatOf(map.Tile).x;
                
                _colonyname = GetColonyName();
                _years = _days / 60;
                _colonistnumber = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists.Count;
                _days = GenDate.DaysPassed;
                _dayhour = GenDate.HourOfDay(Find.TickManager.TicksAbs, longitude);
                _quadrum = GenDate.Quadrum(Find.TickManager.TicksAbs, longitude);

                if (map != null)
                {
                    BiomeDef biome = Find.WorldGrid[map.uniqueID].biome;
                }

                RimRPC.Presence.State = BuildString("state");
                RimRPC.Presence.Details = BuildString("details");
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