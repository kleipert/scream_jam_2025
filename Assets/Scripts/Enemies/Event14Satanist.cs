using System;
using Events;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Event14Satanist : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public bool isAttacking = false;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject eventObject;
        private float _attackCooldownBase = 3f;
        private float _attackCooldownCurrent = 3f;
        private Animator _anim;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (isAttacking)
            {
                _agent.destination = player.transform.position;
                if (Vector3.Distance(transform.position, player.transform.position) <= 1f && _attackCooldownCurrent <= 0f)
                {
                    AttackPlayer();
                    _attackCooldownCurrent = _attackCooldownBase;
                }
                _attackCooldownCurrent -= Time.deltaTime;
            }
        }

        public void AttackPlayer()
        {
            eventObject?.GetComponent<Event_14_SatanistsAttack>()?.IncreaseAttackCount();
            _anim.SetTrigger("Attack");
            PlayerHealth.instance.DoDamageToPlayer();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("HolyWaterBottle"))
            {
                if (other.GetComponent<Event14HolyWater>()._hitCount == 1)
                {
                    eventObject?.GetComponent<Event_14_SatanistsAttack>()?.RemoveSatanist(this.gameObject);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}