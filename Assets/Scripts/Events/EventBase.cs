using GameManager;
using UnityEngine;

namespace Events
{
    public class EventBase : MonoBehaviour
    {
        [SerializeField] protected Data.Events Event;
        [SerializeField] protected Data.PlayerItems Item;
        [SerializeField] protected bool EventDone = false;
        [SerializeField] protected bool EventStarted = false;
        

        public virtual void StartEvent()
        {
            this.gameObject.SetActive(true);
            EventController.instance.isEventActive = true;
            EventStarted = true;
        }
        
        public virtual void StopEvent()
        {
            EventController.instance.isEventActive = false;
            EventController.instance.lastEvent = Event;
            EventStarted = false;
            EventDone = true;
            E_Button.Instance.HideButton();
            PlayerItemController.instance.ClearPlayerItem();
            this.gameObject.SetActive(false);
        }

        public Data.Events GetEvent() => Event;
        public Data.PlayerItems GetItem() => Item;
        public bool IsEventDone() => EventDone;
    }
}