using System.Collections;
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
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject satanistsParentObj;
        [SerializeField] private AudioSource audioScream;
        [SerializeField] private AudioSource audioDoor;

        private Event14PlayerData playerData;
        private bool satanistsSpawned = false;
        private bool doorOpen = false;
        private uint _atkCount = 0;
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.SatanistsAttack;
            Item = Data.EventsToItemsMap[Event];
            audioScream.Play();
            StartCoroutine(OpenDoor());
        }
        
        private void Update()
        {
            if (!doorOpen || !EventStarted) return;

            if (!satanistsSpawned)
            {
                satanistsParentObj.SetActive(true);
                foreach (var satanist in satanists)
                {
                    satanist.SetActive(true);
                    satanist.GetComponent<Event14Satanist>().isAttacking = true;
                    satanist.GetComponent<AudioSource>().Play();
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

        IEnumerator OpenDoor()
        {
            yield return new WaitForSeconds(5f);
            
            _door.transform.Rotate(Vector3.up, _rotOpen);
            audioDoor.Play();
            doorOpen = true;
        }
    }
}
