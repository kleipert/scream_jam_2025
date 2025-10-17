using System.Collections;
using UnityEngine;
using GameManager;

namespace Events
{
    public class Event_09_DemonThrowsUp : EventBase
    {
        [SerializeField] private GameObject spine;
        [SerializeField] private GameObject head;
        [SerializeField] private GameObject laser;
        
        [SerializeField] float spineDegPerSec = 30f;
        [SerializeField] float headDegPerSec = 150f;
        
        [SerializeField] float targetSpineZ = -100f;
        
        [SerializeField] float angleTolerance = 0.5f;

        private bool _startEvent;
        private bool _spineAtTarget;
        private bool _isBeaming = false;
        
        Quaternion baseSpineLocal, baseHeadLocal;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.DemonThrowsUp;
            Item = Data.EventsToItemsMap[Event];
            
            if (spine) baseSpineLocal = spine.transform.localRotation;
            if (head)  baseHeadLocal  = head.transform.localRotation;
            
            _startEvent = true;
        }

        void LateUpdate()
        {
            if (_startEvent)
            {
                if (spine != null && !_spineAtTarget)
                {
                    var e = spine.transform.localEulerAngles;
                    e.z = Mathf.MoveTowardsAngle(e.z, targetSpineZ, spineDegPerSec * Time.deltaTime);
                    float nextZ = Mathf.MoveTowardsAngle(e.z, targetSpineZ, spineDegPerSec * Time.deltaTime);
                    e.z = nextZ;
                    spine.transform.localEulerAngles = e;
                    
                    _spineAtTarget = Mathf.Abs(Mathf.DeltaAngle(nextZ, targetSpineZ)) <= angleTolerance;

                    if (_spineAtTarget && !_isBeaming)
                    {
                        _isBeaming = true;
                        head.GetComponent<BoxCollider>().enabled = true;
                        laser.gameObject.SetActive(true);
                        StartCoroutine(EndEvent());
                    }
                }

                if (head != null && _spineAtTarget)
                {
                    head.transform.Rotate(0f, headDegPerSec * Time.deltaTime, 0f, Space.Self);
                }
            }
        }

        IEnumerator EndEvent()
        {
            yield return new WaitForSeconds(10.0f);
            head.GetComponent<BoxCollider>().enabled = false;
            EventController.instance.StopCurrentEvent();
        } 

        public override void StopEvent()
        {
            laser.gameObject.SetActive(false);
            _startEvent = false;
            
            StartCoroutine(ReturnToBaseThenFinish());
        }
        
        IEnumerator ReturnToBaseThenFinish()
        {
            while (true)
            {
                bool spineDone = true, headDone = true;

                if (spine != null)
                {
                    spine.transform.localRotation = Quaternion.RotateTowards(
                        spine.transform.localRotation, baseSpineLocal, spineDegPerSec * Time.deltaTime);
                    spineDone = Quaternion.Angle(spine.transform.localRotation, baseSpineLocal) <= angleTolerance;
                }

                if (head != null)
                {
                    head.transform.localRotation = Quaternion.RotateTowards(
                        head.transform.localRotation, baseHeadLocal, headDegPerSec * Time.deltaTime);
                    headDone = Quaternion.Angle(head.transform.localRotation, baseHeadLocal) <= angleTolerance;
                }

                if (spineDone && headDone) break;
                yield return null;
            }
            base.StopEvent();
        }
    }
}
