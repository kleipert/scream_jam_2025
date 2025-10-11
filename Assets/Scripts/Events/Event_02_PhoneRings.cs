namespace Events
{
    public class Event_02_PhoneRings : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.PhoneRings;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
