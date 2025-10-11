namespace Events
{
    public class Event_11_GhostsAttack : EventBase
    {
        public override void Start()
        {
            base.Start();
            Event = Data.Events.GhostsAttack;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
