using System;
using System.Collections;
using GameManager;
using UnityEngine;

namespace Events
{
    public class Event_02_PhoneRings : EventBase
    {
        [SerializeField] private GameObject _phoneCollider;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PhoneRings;
            Item = Data.EventsToItemsMap[Event];
            _phoneCollider.gameObject.SetActive(true);
        }

        public override void StopEvent()
        {
            base.StopEvent();
            _phoneCollider.gameObject.SetActive(false);
        }
    }
}
