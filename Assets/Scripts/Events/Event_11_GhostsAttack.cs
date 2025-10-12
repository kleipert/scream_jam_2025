using System;
using System.Collections.Generic;
using Enemies;
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
            var ghostToAttack = Random.Range(0, ghosts.Count);
            
            if(!playerData.getsAttacked)    
                AttackPlayer(ghostToAttack);
        }

        private void AttackPlayer(int ghostIdx)
        {
            playerData.getsAttacked = true;
            ghosts[ghostIdx].GetComponent<Event11Ghosts>().isAttacking = true;
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
