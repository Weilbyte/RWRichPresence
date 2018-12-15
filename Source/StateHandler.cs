using RimWorld;
using Verse;
using RimWorld.Planet;
using System.Linq;

namespace RimRPC
{
    public class StateHandler
    {
        internal static string colonyname;
        internal static int years;
        internal static int days;
        internal static int dayhour;
        internal static float colonistnumber;

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
            //Log.Message("[RichPresence] Pushed presence update to RPC."); commented to remove log spam
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
                days = GenDate.DaysPassed;
                dayhour = GenDate.HourOfDay(Find.TickManager.TicksAbs, longitude);
                colonistnumber = (float)PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists.Count<Pawn>();
                //Season season = GenDate.Season(Find.TickManager.TicksAbs, latitude, longitude); 
                //Quadrum updates seem enough.
                Quadrum quadrum = GenDate.Quadrum(Find.TickManager.TicksAbs, longitude);

                BiomeDef biome = Find.WorldGrid[map.uniqueID].biome;
                RimRPC.prsnc.state = "Year " + years + " Day " + days + " (" + dayhour + "h) | " + quadrum;
                RimRPC.prsnc.details = colonyname + ", " + colonistnumber + " Colonists"; 
                RimRPC.prsnc.largeImageText = "RimWorld";
                RimRPC.prsnc.smallImageKey = "inmap";
                RimRPC.prsnc.smallImageText = "Playing!";
            }
            DiscordRPC.UpdatePresence(ref RimRPC.prsnc);
            //Log.Message("[RichPressence] Pushed presence update to RPC."); commented to remove log spam
        }
    }
}