using UnityEngine;

namespace Events
{
    public class EventBase : MonoBehaviour
    {
        protected Data.Events Event;
        protected Data.PlayerItems Item;

        public virtual void Start()
        {
            Event = Data.Events.None;
            Item = Data.EventsToItemsMap[Event];
            this.gameObject.SetActive(true);
        }
        
        public virtual void Stop()
        {
            this.gameObject.SetActive(false);
        }

        public Data.Events GetEvent() => Event;
        public Data.PlayerItems GetItem() => Item;
    }
}