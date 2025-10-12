using System;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Event11Ghosts : MonoBehaviour
    {
        private Transform _startPos;
        private NavMeshAgent _agent;
        public bool isAttacking = false;
        [SerializeField] private GameObject player;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _startPos = transform;
        }

        private void Update()
        {
            if (isAttacking)
            {
                _agent.destination = player.transform.position;
                if (Vector3.Distance(transform.position, player.transform.position) <= .1)
                {
                    _agent.destination = _startPos.position;
                    isAttacking = false;
                    player.GetComponent<Event11PlayerData>().getsAttacked = false;
                }
            }
        }
    }
}