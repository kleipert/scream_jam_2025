namespace Events
{
    public class Event_01_DemonThrowsItems : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.DemonThrowsItems;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
