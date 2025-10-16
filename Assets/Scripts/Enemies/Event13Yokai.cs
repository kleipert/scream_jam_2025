using System;
using UnityEngine;
using UnityEngine.AI;

public class Event13Yokai : MonoBehaviour
{
    [SerializeField] private float _growthSpeed = 1.25f;
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource yokaiSound;
    private NavMeshAgent _agent;
    

    private bool navMeshActive = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        yokaiSound.Play();
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * _growthSpeed);
        if (transform.localScale is { x: >= .95f, y: >= .95f, z: >= .95f } && !navMeshActive)
        {
            _agent.enabled = true;
            navMeshActive = true;
        }
        if(_agent.isOnNavMesh)
            _agent.destination = _player.transform.position;
    }
}
