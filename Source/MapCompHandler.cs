using Verse;

namespace RimRPC
{
    internal class MapCompHandler : MapComponent
    {
        private int pushcooldown = 0;

        public MapCompHandler(Map map) : base(map)
        {
            this.map = map;
        }

        public override void MapComponentTick()
        {
            //We use this variable to avoid lag. Update is called every 1250 ticks. Thats twice each ingame hour.
            pushcooldown = pushcooldown + 1;
            if (pushcooldown > 1250)
            {
                StateHandler.PushState(map);
                pushcooldown = 0;
            }
            base.MapComponentTick();
        }
    }
}