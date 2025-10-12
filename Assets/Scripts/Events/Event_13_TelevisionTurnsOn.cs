namespace Events
{
    public class Event_13_TelevisionTurnsOn : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TelevisionTurnsOn;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
