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
                //Log.Message("[RichPressence] Calling presence update..."); commented to remove log spam
                pushcooldown = 0;
            }
            base.MapComponentTick();
        }
    }
}