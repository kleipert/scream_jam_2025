using UnityEngine;
using System.Collections;
using GameManager;
using UnityEngine.SceneManagement;

namespace Events
{
    public class Event_18_Credits : EventBase
    {
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TrueName;
            Item = Data.EventsToItemsMap[Event];

            if (EventController.instance.lastEvent == Data.Events.TrueName)
            {
                SceneManager.LoadScene("TrueName"); 
            }
            else
            {
                SceneManager.LoadScene("NormalEnding"); 
            }
        }
        
        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}