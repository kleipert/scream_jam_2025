using System;
using System.Collections;
using GameManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Events
{
    public class Event_02_PhoneRings : EventBase
    {
        [SerializeField] private GameObject phoneCollider;
        private AudioSource _audioSource;
        
        public override void StartEvent()
        {
            _audioSource = GetComponent<AudioSource>();
            base.StartEvent();
            Event = Data.Events.PhoneRings;
            Item = Data.EventsToItemsMap[Event];
            phoneCollider.gameObject.SetActive(true);
            _audioSource.Play();
        }

        public override void StopEvent()
        {
            base.StopEvent();
            phoneCollider.gameObject.SetActive(false);
        }

        public void StopAudio()
        {
            _audioSource.Stop();
        }
    }
}
