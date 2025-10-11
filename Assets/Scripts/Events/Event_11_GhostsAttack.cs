namespace Events
{
    public class Event_11_GhostsAttack : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.GhostsAttack;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
