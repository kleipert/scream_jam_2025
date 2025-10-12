namespace Events
{
    public class Event_04_FindCursedItem : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.FindCursedItem;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
