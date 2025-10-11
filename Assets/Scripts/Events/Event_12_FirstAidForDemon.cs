namespace Events
{
    public class Event_12_FirstAidForDemon : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.FirstAidForDemon;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
