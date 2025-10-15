using UnityEngine;

namespace Events
{
    public class Event_13_TelevisionTurnsOn : EventBase
    {
        [SerializeField] private GameObject yokai;
        [SerializeField] private GameObject tv_usual;
        [SerializeField] private GameObject tv_flicker;
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TelevisionTurnsOn;
            Item = Data.EventsToItemsMap[Event];
            tv_usual.SetActive(false);
            tv_flicker.SetActive(true);
            yokai.SetActive(true);
        }

        public void TurnOffTV()
        {
            yokai.SetActive(false);
            tv_flicker.SetActive(false);
            tv_usual.SetActive(true);
            StopEvent();
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
