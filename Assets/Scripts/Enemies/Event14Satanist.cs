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
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (isAttacking)
            {
                _agent.destination = player.transform.position;
                if (Vector3.Distance(transform.position, player.transform.position) <= 1 && _attackCooldownCurrent <= 0f)
                {
                    AttackPlayer();
                    _attackCooldownCurrent = _attackCooldownBase;
                }

                _attackCooldownCurrent -= Time.deltaTime;
            }
        }

        public void AttackPlayer()
        {
            
        }

        private void OnDestroy()
        {
            //eventObject.GetComponent<Event_14_SatanistsAttack>().RemoveGhost(this.gameObject);
            player.GetComponent<Event14PlayerData>().getsAttacked = false;
        }
    }
}