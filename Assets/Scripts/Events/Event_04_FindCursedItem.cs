using UnityEngine;

namespace Events
{
    public class Event_04_FindCursedItem : EventBase
    {
        [SerializeField] private GameObject cursedCollider;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.FindCursedItem;
            Item = Data.EventsToItemsMap[Event];
            cursedCollider.gameObject.SetActive(true);
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
