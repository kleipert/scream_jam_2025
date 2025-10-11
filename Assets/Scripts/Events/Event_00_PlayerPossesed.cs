namespace Events
{
    public class Event_00_PlayerPossesed : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.PlayerPossesed;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
