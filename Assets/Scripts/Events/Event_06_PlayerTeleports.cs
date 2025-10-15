using System.Collections;
using UnityEngine;

namespace Events
{
    public class Event_06_PlayerTeleports : EventBase
    {
        [SerializeField] private GameObject playerTeleport;
        [SerializeField] private GameObject player;
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.PlayerTeleports;
            Item = Data.EventsToItemsMap[Event];
            StartCoroutine(WaitWithTeleport());
        }

        private IEnumerator WaitWithTeleport()
        {
            yield return new WaitForSeconds(3f);
            player.transform.position = playerTeleport.transform.position;
            StopEvent();
        }


        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
