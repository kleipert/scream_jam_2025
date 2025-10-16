using System;
using System.Collections;
using GameManager;
using UnityEngine;

namespace Events
{
    public class Event_10_TieDemonDown : EventBase
    {
        [SerializeField] private GameObject LeftLine;
        [SerializeField] private GameObject RightLine;
        [SerializeField] private AudioClip clipTied;
        private bool isLeftTied = false;
        private bool isRightTied = false;
        private bool _leftAudio;
        private bool _rightAudio;
        private bool _audioPlayed = true;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TieDownDemon;
            Item = Data.EventsToItemsMap[Event];
        }

        private void Update()
        {
            if (!PlayerItemController.instance.hasItem || !EventStarted) return;

            if (isLeftTied)
            {
                StartCoroutine(ActivateSound(true));
            }
            
            if (isRightTied)
            {
                StartCoroutine(ActivateSound(false));
            }
            
            if (_leftAudio)
                LeftLine.GetComponent<Event_10_Ropes>().Activate();
            if (_rightAudio)
                RightLine.GetComponent<Event_10_Ropes>().Activate();
            if (isRightTied && isLeftTied)
                StartCoroutine(EndAfterDelay());

        }

        private IEnumerator EndAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            StopEvent();
        }

        private IEnumerator ActivateSound(bool kevin)
        {
            if (!_audioPlayed)
                SoundManager.Instance.PlaySound(clipTied,transform,0.3f);
            _audioPlayed  = true;
            yield return new WaitForSeconds(2f);
            if (kevin)
                _leftAudio = true;
            else
                _rightAudio = true;
        }

        public void TieDownRight()
        {
            isRightTied = true;
            _audioPlayed = false;
        }
        
        public void TieDownLeft()
        {
            isLeftTied = true;
            _audioPlayed = false;
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
