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
        [SerializeField] private GameObject ghostsParentObj;
        [SerializeField] private AudioClip ghostsSound;
        [SerializeField] private AudioClip ghostSpawn;

        private Event11PlayerData playerData;
        private bool ghostsSpawned = false;
        private bool _canAttack;

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
            if (!PlayerItemController.instance.hasItem || !EventStarted) return;

            if (!ghostsSpawned)
            {
                ghostsParentObj.SetActive(true);
                foreach (var ghost in ghosts)
                    ghost.SetActive(true);

                ghostsSpawned = true;
                SoundManager.Instance.PlaySound(ghostSpawn,transform,0.2f);
            }
            
            var ghostToAttack = Random.Range(0, ghosts.Count);

            StartCoroutine(StartAttack());
            
            if(!playerData.getsAttacked && ghosts.Count > 0 && _canAttack)
                AttackPlayer(ghostToAttack);
        }

        private void AttackPlayer(int ghostIdx)
        {
            playerData.getsAttacked = true;
            ghosts[ghostIdx].GetComponent<Event11Ghosts>().isAttacking = true;
            SoundManager.Instance.PlaySound(ghostsSound,transform,0.3f);
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

        private IEnumerator StartAttack()
        {
            yield return new WaitForSeconds(3f);
            _canAttack =  true;
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
