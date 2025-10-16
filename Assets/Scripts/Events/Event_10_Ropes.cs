using UnityEngine;

namespace Events
{
    public class Event_10_Ropes : MonoBehaviour
    {
        [SerializeField] private GameObject _bedPos;
        [SerializeField] private GameObject _demonPos;
        [SerializeField] private GameObject _colliderObj;
        
        private LineRenderer _lr;
        private bool _isActivated = false;

        private void Start()
        {
            _lr = GetComponent<LineRenderer>();
            _lr.positionCount = 2;
        }

        private void Update()
        {
            if (_isActivated)
            {
                _lr.SetPosition(0, _bedPos.transform.position);      
                _lr.SetPosition(1, _demonPos.transform.position);    
            }
            
        }

        public void Activate()
        {
            _lr.enabled = true;
            _isActivated = true;
            _colliderObj.GetComponent<BoxCollider>().enabled = false;

        }

        public void Disable()
        {
            _lr.enabled = false;
        }
    }
}