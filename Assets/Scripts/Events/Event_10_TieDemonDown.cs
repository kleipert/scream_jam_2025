namespace Events
{
    public class Event_10_TieDemonDown : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TieDownDemon;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
