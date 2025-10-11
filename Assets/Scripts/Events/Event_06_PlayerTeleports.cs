namespace Events
{
    public class Event_06_PlayerTeleports : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.PlayerTeleports;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
