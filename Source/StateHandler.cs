using RimWorld;
using Verse;

namespace RimRPC
{
    public class StateHandler
    {
        internal static string colonyname;
        internal static int days;
        internal static int dayhour;

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
                days = GenDate.DaysPassed;
                dayhour = GenDate.HourOfDay(Find.TickManager.TicksAbs, longitude);
                //Season season = GenDate.Season(Find.TickManager.TicksAbs, latitude, longitude); 
                //Quadrum updates seem enough.
                Quadrum quadrum = GenDate.Quadrum(Find.TickManager.TicksAbs, longitude);

                BiomeDef biome = Find.WorldGrid[map.uniqueID].biome;
                RimRPC.prsnc.state = "Day " + days + " (" + dayhour + "h) | " + quadrum;
                RimRPC.prsnc.details = colonyname;
                RimRPC.prsnc.largeImageText = "RimWorld";
                RimRPC.prsnc.smallImageKey = "inmap";
                RimRPC.prsnc.smallImageText = "Playing!";
            }
            DiscordRPC.UpdatePresence(ref RimRPC.prsnc);
        }
    }
}