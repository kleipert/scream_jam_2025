using UnityEngine;

namespace Events
{
    public class Event_12_FirstAidForDemon : EventBase
    {
        [SerializeField] private GameObject colliderHeal;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.FirstAidForDemon;
            Item = Data.EventsToItemsMap[Event];
            
            colliderHeal.gameObject.SetActive(true);
        }

        public override void StopEvent()
        {
            base.StopEvent();
            colliderHeal.gameObject.SetActive(false);
        }
    }
}
