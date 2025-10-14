using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Events
{
    public class Event_14_SatanistsAttack : EventBase
    {
        [SerializeField] private GameObject _door;
        private float _rotOpen = 120f;
        private float _rotClosed = 0f;
        
        [SerializeField] private List<GameObject> satanists;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject satanitsParentObj;

        private Event14PlayerData playerData;
        private bool ghostsSpawned = false;
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.SatanistsAttack;
            Item = Data.EventsToItemsMap[Event];
            _door.transform.Rotate(Vector3.up, _rotOpen);

        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
