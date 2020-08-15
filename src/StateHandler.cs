using RimWorld;
using System;
using System.Linq;
using Verse;

namespace RimRPC
{
    public class StateHandler
    {
        internal static string colonyname;
        internal static int years;
        internal static int days;
        internal static int dayhour;
        internal static float colonistnumber;
        internal static Quadrum quadrum;

        public static string GetColonyName()
        {
            return Faction.OfPlayer.Name;
        }

        public static int GetDaysSurvived()
        {
            long ticks = Find.TickManager.TicksAbs;
            int num = (int)(ticks / 60000L);
            if (num < 0L)
            {
                ticks--;
            }
            return num;
        }

        public static void MenuState()
        {
            RimRPC.prsnc.details = "Main Menu";
            RimRPC.prsnc.state = null;
            RimRPC.prsnc.smallImageKey = null;
            RimRPC.prsnc.smallImageText = null;
            DiscordRPC.UpdatePresence(ref RimRPC.prsnc);
        }
        
        public static string BuildString(string which)
        {
            if (which == "state")
            {
                string state = "";
                if (RWRPCMod.settings.RPC_CustomBottom)
                {
                    return RWRPCMod.settings.RPC_CustomBottomText;
                }
                if (RWRPCMod.settings.RPC_Day)
                {
                    state += "Day " + days;
                }
                if (RWRPCMod.settings.RPC_Day && RWRPCMod.settings.RPC_Hour)
                {
                    state += " (" + dayhour + "h)";
                }
                if (RWRPCMod.settings.RPC_Quadrum)
                {
                    if (RWRPCMod.settings.RPC_Day)
                    {
                        state += " | ";
                    }
                    state += quadrum;
                }
                if (RWRPCMod.settings.RPC_Year)
                {
                    if (RWRPCMod.settings.RPC_Quadrum || RWRPCMod.settings.RPC_Day)
                    {
                        state += " | ";
                    }
                    if (RWRPCMod.settings.RPC_YearShort)
                    {
                        state += "y" + years;
                        return state;
                    }
                    state += "Year " + years;
                }
                return state;
                
            }
            if (which == "details")
            {
                if (RWRPCMod.settings.RPC_CustomTop)
                {
                    return RWRPCMod.settings.RPC_CustomTopText;
                }
                if (RWRPCMod.settings.RPC_Colony)
                {
                    if (RWRPCMod.settings.RPC_ColonistCount)
                    {
                        return colonyname + " (" + colonistnumber + ")";
                    }
                    return colonyname;
                }
                if (RWRPCMod.settings.RPC_ColonistCount)
                {
                    return colonistnumber + " Colonists";
                }
            }
            return null;
        }

        public static void PushState(Map map)
        {
            var world = Current.Game != null ? Current.Game.World : null;
            if (world == null)
            {
                RimRPC.prsnc.details = "Main Menu";
            }
            else
            {
                float latitude = (map == null) ? 0f : Find.WorldGrid.LongLatOf(map.Tile).y;
                float longitude = (map == null) ? 0f : Find.WorldGrid.LongLatOf(map.Tile).x;
                colonyname = GetColonyName();
                years = days / 60;
                colonistnumber = (float)PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists.Count<Pawn>();
                days = GenDate.DaysPassed;
                dayhour = GenDate.HourOfDay(Find.TickManager.TicksAbs, longitude);
                quadrum = GenDate.Quadrum(Find.TickManager.TicksAbs, longitude);

                BiomeDef biome = Find.WorldGrid[map.uniqueID].biome;
                RimRPC.prsnc.state = BuildString("state");
                RimRPC.prsnc.details = BuildString("details");
                RimRPC.prsnc.largeImageText = "RimWorld";
                RimRPC.prsnc.smallImageKey = "inmap";
                RimRPC.prsnc.smallImageText = "Playing!";
                if (RWRPCMod.settings.RPC_Time)
                {
                    RimRPC.prsnc.startTimestamp = RimRPC.started;
                }
            }
            DiscordRPC.UpdatePresence(ref RimRPC.prsnc);
        }
    }
}