using System.Collections;
using UnityEngine;

namespace Events
{
    public class Event_05_DemonFlies : EventBase
    {
        [SerializeField] private GameObject colliderFlying;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.DemonFlies;
            Item = Data.EventsToItemsMap[Event];
            
            colliderFlying.gameObject.SetActive(true);
        }

        public override void StopEvent()
        {
            base.StopEvent();
            colliderFlying.gameObject.SetActive(false);
        }
    }
}
