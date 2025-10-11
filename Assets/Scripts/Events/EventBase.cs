using UnityEngine;

namespace Events
{
    public class EventBase : MonoBehaviour
    {
        [SerializeField] protected Data.Events Event;
        [SerializeField] protected Data.PlayerItems Item;

        public virtual void StartEvent()
        {
            this.gameObject.SetActive(true);
        }
        
        public virtual void StopEvent()
        {
            this.gameObject.SetActive(false);
        }

        public Data.Events GetEvent() => Event;
        public Data.PlayerItems GetItem() => Item;
    }
}