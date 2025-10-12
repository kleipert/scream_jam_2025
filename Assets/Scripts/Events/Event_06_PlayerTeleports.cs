namespace Events
{
    public class Event_06_PlayerTeleports : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PlayerTeleports;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
