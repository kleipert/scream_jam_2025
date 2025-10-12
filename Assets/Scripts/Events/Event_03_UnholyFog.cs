using System;
using UnityEngine;

namespace Events
{
    public class Event_03_UnholyFog : EventBase
    {
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.UnholyFog;
            Item = Data.EventsToItemsMap[Event];
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
