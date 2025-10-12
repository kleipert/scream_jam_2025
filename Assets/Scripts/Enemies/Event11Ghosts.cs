using System;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Event11Ghosts : MonoBehaviour
    {
        private Vector3 _startPos;
        private NavMeshAgent _agent;
        public bool isAttacking = false;
        [SerializeField] private GameObject player;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _startPos = transform.position;
        }

        private void Update()
        {
            if (isAttacking)
            {
                _agent.destination = player.transform.position;
                if (Vector3.Distance(transform.position, player.transform.position) <= .3)
                {
                    _agent.destination = _startPos;
                    isAttacking = false;
                    player.GetComponent<Event11PlayerData>().getsAttacked = false;
                }
            }
        }

        private void OnDestroy()
        {
            
        }
    }
}