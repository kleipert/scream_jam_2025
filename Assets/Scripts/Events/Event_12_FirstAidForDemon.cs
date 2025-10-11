namespace Events
{
    public class Event_12_FirstAidForDemon : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.FirstAidForDemon;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
