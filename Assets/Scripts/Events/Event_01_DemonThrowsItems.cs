using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Events
{
    public class Event_01_DemonThrowsItems : EventBase
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private List<GameObject> throwableItems;
        private bool _readyToThrow = false;
        private float _throwCooldownBase = 2f;
        private float _currentThrowCooldown = 0f;
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.DemonThrowsItems;
            Item = Data.EventsToItemsMap[Event];

            StartCoroutine(DelayStart());
        }

        private IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(2f);
            _readyToThrow = true;
        }

        private void Update()
        {
            if (!_readyToThrow) return;
            
            var itemToThrow = Random.Range(0, throwableItems.Count);
            if (throwableItems.Count > 0 && _currentThrowCooldown <= 0f)
            {
                throwableItems[itemToThrow].GetComponent<Event01ThrowableItem>().ThrowAtPlayer(_player.transform.position);
                _currentThrowCooldown = _throwCooldownBase;
            }

            _currentThrowCooldown -= Time.deltaTime;
        }
        
        public void RemoveThrowable(GameObject item)
        {
            throwableItems.Remove(item);
            if (throwableItems.Count <= 0)
            {
                EventDone = true;
                StopEvent();
            }
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
