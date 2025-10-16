using System.Collections;
using UnityEngine;

namespace Events
{
    public class Event_13_TelevisionTurnsOn : EventBase
    {
        [SerializeField] private GameObject yokai;
        [SerializeField] private GameObject tv_usual;
        [SerializeField] private GameObject tv_flicker;
        [SerializeField] private AudioSource audioTV;
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TelevisionTurnsOn;
            Item = Data.EventsToItemsMap[Event];
            tv_usual.SetActive(false);
            tv_flicker.SetActive(true);
            audioTV.Play();
            StartCoroutine(WaitDemon());
        }

        public void TurnOffTV()
        {
            yokai.SetActive(false);
            tv_flicker.SetActive(false);
            audioTV.Stop();
            tv_usual.SetActive(true);
            StopEvent();
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }

        private IEnumerator WaitDemon()
        {
            yield return new WaitForSeconds(2f);
            yokai.SetActive(true);
        }
    }
}
