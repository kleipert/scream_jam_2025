namespace Events
{
    public class Event_08_PortalOpens : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.PortalOpens;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
