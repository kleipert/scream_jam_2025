using Player;
using UnityEngine;

namespace GameManager
{
    public class PlayerItemController : MonoBehaviour
    {
        public static PlayerItemController instance;
        public bool hasItem = false;

        [SerializeField] private PlayerInteraction playerInteraction;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
            instance = this;
        }

        public void GetCorrectPlayerItem()
        {
            var activeEvent = EventController.instance.GetActiveEvent();
            var eventItem = Data.EventsToItemsMap[activeEvent];
            playerInteraction.SetActiveItem(eventItem);
            hasItem = true;
        }

        public void ClearPlayerItem()
        {
            playerInteraction.RemoveActiveItem();
            hasItem = false;
        }
    }
}
