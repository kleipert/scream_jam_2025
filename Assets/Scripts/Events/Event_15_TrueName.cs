namespace Events
{
    public class Event_15_TrueName : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TrueName;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
