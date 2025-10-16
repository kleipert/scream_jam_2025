using UnityEngine;
using System.Collections;
using GameManager;

namespace Events
{
    public class Event_15_TrueName : EventBase
    {
        [SerializeField] private GameObject lightning;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TrueName;
            Item = Data.EventsToItemsMap[Event];
            StartCoroutine(EndEvent());
        }
        
        private IEnumerator EndEvent()
        {
            yield return new WaitForSeconds(6f);
            lightning.SetActive(true);
            EventController.instance.StopCurrentEvent();
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
