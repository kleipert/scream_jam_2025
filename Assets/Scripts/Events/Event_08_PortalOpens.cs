namespace Events
{
    public class Event_08_PortalOpens : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PortalOpens;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
