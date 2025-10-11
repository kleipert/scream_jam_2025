namespace Events
{
    public class Event_14_SatanistsAttack : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.SatanistsAttack;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
