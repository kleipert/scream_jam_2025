using System;
using UnityEngine;
using System.Collections;
using GameManager;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Events
{
    public class Event_18_Credits : EventBase
    {
        [SerializeField] private Image image;
        [SerializeField] private float duration;
        
        public override void StartEvent()
        {
            base.StartEvent();
            Event = Data.Events.TrueName;
            Item = Data.EventsToItemsMap[Event];

            StartCoroutine(FadeRoutine());
        }
        
        public override void StopEvent()
        {
            base.StopEvent();
        }

        IEnumerator FadeRoutine()
        {
            float start = image.color.a;
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;  
                float a = Mathf.Lerp(start, 1, t / duration);
                var c = image.color; c.a = a; image.color = c;
                yield return null;
            }
            var final = image.color; final.a = 1; image.color = final;
            ChangeScene();
        }

        private void ChangeScene()
        {
            if (EventController.instance.lastEvent == Data.Events.TrueName)
            {
                SceneManager.LoadScene("TrueName"); 
            }
            else
            {
                SceneManager.LoadScene("NormalEnding"); 
            }
        }
    }
}