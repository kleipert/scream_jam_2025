namespace Events
{
    public class Event_07_LightsOut : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.LightsOut;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
