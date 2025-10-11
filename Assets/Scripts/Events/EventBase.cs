using UnityEngine;

namespace Events
{
    public class EventBase : MonoBehaviour
    {
        [SerializeField] protected Data.Events Event;
        [SerializeField] protected Data.PlayerItems Item;
        [SerializeField] protected bool EventDone = false;
        

        public virtual void StartEvent()
        {
            this.gameObject.SetActive(true);
        }
        
        public virtual void StopEvent()
        {
            EventDone = true;
            this.gameObject.SetActive(false);
        }

        public Data.Events GetEvent() => Event;
        public Data.PlayerItems GetItem() => Item;
        public bool IsEventDone() => EventDone;
    }
}