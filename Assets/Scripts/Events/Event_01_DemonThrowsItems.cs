namespace Events
{
    public class Event_01_DemonThrowsItems : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.DemonThrowsItems;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
