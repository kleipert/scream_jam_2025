namespace Events
{
    public class Event_04_FindCursedItem : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.FindCursedItem;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
