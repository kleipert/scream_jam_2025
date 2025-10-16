using System.Collections;
using UnityEngine;
using StarterAssets;

namespace Events
{
    public class Event_06_PlayerTeleports : EventBase
    {
        [SerializeField] private GameObject playerTeleport;
        [SerializeField] private GameObject player;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private StarterAssetsInputs input;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PlayerTeleports;
            Item = Data.EventsToItemsMap[Event];
            StartCoroutine(WaitWithTeleport());
        }

        private IEnumerator WaitWithTeleport()
        {
            audioSource.Play();
            yield return new WaitForSeconds(2f);
            input.canMove = false;
            player.transform.position = playerTeleport.transform.position;
            input.canMove = true;
            StopEvent();
        }


        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
