using System;
using System.Collections;
using GameManager;
using UnityEngine;

namespace Events
{
    public class Event_03_UnholyFog : EventBase
    {
        [SerializeField] private GameObject[] fogs;
        private bool fogActive;
        private Animation _senserAnimation;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.UnholyFog;
            Item = Data.EventsToItemsMap[Event];
            _senserAnimation = new Animation();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            foreach (var fog in fogs)
            {
                fog.SetActive(true);
            }
        }
    }
}
