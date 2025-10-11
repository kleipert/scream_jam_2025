namespace Events
{
    public class Event_13_TelevisionTurnsOn : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.TelevisionTurnsOn;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
