namespace Events
{
    public class Event_00_PlayerPossesed : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PlayerPossesed;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
