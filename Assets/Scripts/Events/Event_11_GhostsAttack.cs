using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using GameManager;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Events
{
    public class Event_11_GhostsAttack : EventBase
    {

        [SerializeField] private List<GameObject> ghosts;
        [SerializeField] private GameObject player;
        private Event11PlayerData playerData;
        private bool ghostsSpawned = false;

        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.GhostsAttack;
            Item = Data.EventsToItemsMap[Event];
        }

        private void Start()
        {
            playerData = player.GetComponent<Event11PlayerData>();
        }

        private void Update()
        {
            if (!PlayerItemController.instance.hasItem) return;

            if (!ghostsSpawned)
            {
                foreach (var ghost in ghosts)
                    ghost.SetActive(true);

                ghostsSpawned = true;
                StartCoroutine(Wait());

                IEnumerator Wait()
                {
                    yield return new WaitForSeconds(2);
                }
            }
            
            var ghostToAttack = Random.Range(0, ghosts.Count);
            
            if(!playerData.getsAttacked && ghosts.Count > 0)
                AttackPlayer(ghostToAttack);
        }

        private void AttackPlayer(int ghostIdx)
        {
            playerData.getsAttacked = true;
            ghosts[ghostIdx].GetComponent<Event11Ghosts>().isAttacking = true;
        }

        public void RemoveGhost(GameObject ghost)
        {
            ghosts.Remove(ghost);
            if (ghosts.Count <= 0)
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
