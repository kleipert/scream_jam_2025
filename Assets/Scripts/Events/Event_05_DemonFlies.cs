namespace Events
{
    public class Event_05_DemonFlies : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.DemonFlies;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
