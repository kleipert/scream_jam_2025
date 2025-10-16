using System.Collections.Generic;
using Enemies;
using GameManager;
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
        [SerializeField] private GameObject satanistsParentObj;

        private Event14PlayerData playerData;
        private bool satanistsSpawned = false;
        private uint _atkCount = 0;
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.SatanistsAttack;
            Item = Data.EventsToItemsMap[Event];
            _door.transform.Rotate(Vector3.up, _rotOpen);
        }
        
        private void Update()
        {
            if (!PlayerItemController.instance.hasItem || !EventStarted) return;

            if (!satanistsSpawned)
            {
                satanistsParentObj.SetActive(true);
                foreach (var satanist in satanists)
                {
                    satanist.SetActive(true);
                    satanist.GetComponent<Event14Satanist>().isAttacking = true;
                }
                satanistsSpawned = true;
            }
        }
        
        public void RemoveSatanist(GameObject satanist)
        {
            satanists.Remove(satanist);
            if (satanists.Count <= 0)
            {
                _door.transform.Rotate(Vector3.up, _rotClosed);
                EventDone = true;
                StopEvent();
            }
        }

        public void IncreaseAttackCount() => _atkCount++;

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
