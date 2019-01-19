using Verse;

namespace RimRPC
{
    internal class MapCompHandler : MapComponent
    {
        private int cooldown = 0;

        public MapCompHandler(Map map) : base(map)
        {
            this.map = map;
        }

        public override void MapComponentTick()
        {
            cooldown = cooldown + 1;
            if (cooldown > 1250) //Update is called every 1250 ticks (Twice an in-game hour, or every 20.5 seconds real-time)
            {
                StateHandler.PushState(map);
                cooldown = 0;
            }
            base.MapComponentTick();
        }
    }
}