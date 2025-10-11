namespace Events
{
    public class Event_05_DemonFlies : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.DemonFlies;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
