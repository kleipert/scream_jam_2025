using System;
using System.Collections;
using GameManager;
using UnityEngine;
using StarterAssets;

namespace Events
{
    public class Event_00_PlayerPossesed : EventBase
    {
        [SerializeField] StarterAssetsInputs starterAssets;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PlayerPossesed;
            Item = Data.EventsToItemsMap[Event];
            starterAssets.invert = true;
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
