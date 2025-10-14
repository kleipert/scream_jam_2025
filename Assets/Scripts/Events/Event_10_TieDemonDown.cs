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
        private bool isLeftTied = false;
        private bool isRightTied = false;
        
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
                LeftLine.GetComponent<Event_10_Ropes>().Activate();
            }
            
            if (isRightTied)
            {
                RightLine.GetComponent<Event_10_Ropes>().Activate();
            }

            if (isRightTied && isLeftTied)
                StartCoroutine(EndAfterDelay());

        }

        private IEnumerator EndAfterDelay()
        {
            yield return new WaitForSeconds(2f);
            StopEvent();
        }

        public void TieDownRight()
        {
            isRightTied = true;
        }
        
        public void TieDownLeft()
        {
            isLeftTied = true;
        }

        public override void StopEvent()
        {
            base.StopEvent();
        }
    }
}
