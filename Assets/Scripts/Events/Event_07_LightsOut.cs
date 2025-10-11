using System;
using System.Collections;
using UnityEngine;

namespace Events
{
    /// <summary>
    /// 1) Turn off lights +
    /// 2) Activate Pentagrams +
    /// 3) Turn lights back on after few seconds +
    /// 4) Pickup Towel +
    /// 5) Cleanse Pentagrams 
    /// </summary>
    public class Event_07_LightsOut : EventBase
    {
        [SerializeField] private GameObject lightSources;
        [SerializeField] private GameObject[] pentagrams;
        [SerializeField] private float lightTimer = 3f;
        private bool pentagramActive = false;
        private Animation _towelAnimation;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.LightsOut;
            Item = Data.EventsToItemsMap[Event];
            _towelAnimation = GetComponent<Animation>();

            StartCoroutine(InitializeEvent());
        }

        private void Update()
        {
            if (!pentagramActive) return;
        }

        private IEnumerator InitializeEvent()
        {
            yield return new WaitForSeconds(lightTimer);
            lightSources.SetActive(false);
            yield return new WaitForSeconds(lightTimer);
            lightSources.SetActive(true);
            foreach (var pentagram in pentagrams)
            {
                pentagram.SetActive(true);
            }
            pentagramActive = true;
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
