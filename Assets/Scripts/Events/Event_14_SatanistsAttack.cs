namespace Events
{
    public class Event_14_SatanistsAttack : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.SatanistsAttack;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
