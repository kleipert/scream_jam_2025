namespace Events
{
    public class Event_09_DemonThrowsUp : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.DemonThrowsUp;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
