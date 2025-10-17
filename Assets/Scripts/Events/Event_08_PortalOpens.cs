using System.Collections;
using UnityEngine;

namespace Events
{
    public class Event_08_PortalOpens : EventBase
    {
        [SerializeField] private GameObject portal;
        [SerializeField] private GameObject circle;
        [SerializeField] private AudioClip clip;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PortalOpens;
            Item = Data.EventsToItemsMap[Event];
            
            circle.gameObject.SetActive(true);

            StartCoroutine(StartPortal());
        }

        IEnumerator StartPortal()
        {
            yield return new WaitForSeconds(5.0f);
            portal.SetActive(true);
        }

        public override void StopEvent()
        {
            base.StopEvent();
            portal.SetActive(false);
            circle.SetActive(false);
            SoundManager.Instance.PlaySound(clip, transform, 0.3f);
        }
    }
}
