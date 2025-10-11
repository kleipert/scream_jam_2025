namespace Events
{
    public class Event_10_TieDemonDown : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.TieDownDemon;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
