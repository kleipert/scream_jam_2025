using System;
using UnityEngine;

namespace Player
{
    public class Event14HolyWater : MonoBehaviour
    {
        public float forwardSpeed = 8f;   // constant horizontal speed
        public float launchUpwardSpeed = 6f; // initial vertical kick
        public bool autoLaunchOnStart = false;

        Rigidbody rb;
        Vector3 flatForward;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            // Flatten forward so slopes don't add vertical drift
            flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        }

        void Start()
        {
            if (autoLaunchOnStart) Launch();
        }

        public void Launch()
        {
            // Set initial velocity: forward constant + upward impulse
            Vector3 v = flatForward * forwardSpeed + Vector3.up * launchUpwardSpeed;
            rb.linearVelocity = v;
        }

        void FixedUpdate()
        {
            // Keep horizontal speed locked to forward; let gravity change Y naturally.
            Vector3 v = rb.linearVelocity;
            Vector3 horiz = flatForward * forwardSpeed;
            rb.linearVelocity = new Vector3(horiz.x, v.y, horiz.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Event_14_Satanist"))
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }

            if (!other.CompareTag("Player") && !other.CompareTag("HolyWaterBottle"))
            {
                Debug.Log($"Collision found with {other.gameObject.name}");
                Destroy(this.gameObject);
                
            }
        }
    }
}