using Verse;

namespace RimRPC
{
    internal class MapCompHandler : MapComponent
    {
        private int _cooldown;

        public MapCompHandler(Map map) : base(map)
        {
            this.map = map;
        }

        public override void MapComponentTick()
        {
            _cooldown++;

            if (_cooldown > 1250) //Update is called every 1250 ticks (Twice an in-game hour, or every 20.5 seconds real-time)
            {
                StateHandler.PushState(map);
                _cooldown = 0;
            }

            base.MapComponentTick();
        }
    }
}