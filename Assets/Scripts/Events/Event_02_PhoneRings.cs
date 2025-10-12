namespace Events
{
    public class Event_02_PhoneRings : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PhoneRings;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
