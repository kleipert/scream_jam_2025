using System;
using System.Collections;
using GameManager;
using UnityEngine;
using StarterAssets;

namespace Events
{
    public class Event_Normal_Ending : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.NormalEnd;
            Item = Data.EventsToItemsMap[Event];
            EventController.instance.StopCurrentEvent();
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}