namespace Events
{
    public class Event_09_DemonThrowsUp : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.DemonThrowsUp;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
