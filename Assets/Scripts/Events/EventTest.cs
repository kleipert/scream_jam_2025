using UnityEngine;

namespace Events
{
    public class EventTest : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.SatanistsAttack;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}