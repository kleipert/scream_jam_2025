namespace Events
{
    public class Event_15_TrueName : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.TrueName;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
