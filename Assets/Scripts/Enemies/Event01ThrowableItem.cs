using System;
using Events;
using UnityEngine;

public class Event01ThrowableItem : MonoBehaviour
{
    private MeshCollider _meshCollider;
    private float flyingSpeed = 1f;
    private float throwSpeed = 10f;
    private bool isThrown = false;
    [SerializeField] private GameObject eventObject;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClip2;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        _meshCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.localPosition.y - 0) > .2f)
            transform.position += Vector3.up * (flyingSpeed * Time.deltaTime);

        if (isThrown)
        {
            transform.position += transform.forward * (throwSpeed * Time.deltaTime);
        }
    }

    public void ThrowAtPlayer(Vector3 position)
    {
        _meshCollider.enabled = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = (Rigidbody) this.gameObject.AddComponent(typeof(Rigidbody));
        
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
        }
        var targetPos = new Vector3(position.x, position.y - .15f, position.z);
        transform.LookAt(targetPos);
        isThrown = true;
        SoundManager.Instance.PlaySound(audioClip2,transform,0.5f,2.0f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(audioClip,transform,0.5f);
            Destroy(this.gameObject);    
        }
            
        else
        {
            // DO SOMETHING WITH PLAYER?
            SoundManager.Instance.PlaySound(audioClip,transform,0.5f);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        eventObject.GetComponent<Event_01_DemonThrowsItems>().RemoveThrowable(this.gameObject);
    }
}
