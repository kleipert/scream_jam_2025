namespace Events
{
    public class Event_03_UnholyFog : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.UnholyFog;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
