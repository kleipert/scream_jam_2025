using GameManager;
using StarterAssets;
using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] playerItems;
        private StarterAssetsInputs _input;
        private bool _pickupAvailable = false;

        private void Start()
        {
            _input = GetComponentInParent<StarterAssetsInputs>();
        }

        private void Update()
        {
            if (!_pickupAvailable) return;
            if (!_input.interact) return;
            PlayerItemController.instance.GetCorrectPlayerItem();
            _pickupAvailable = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger Enter");
            if (!other.CompareTag("Interactable")) return;
            _pickupAvailable = true;
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Trigger Exit");
            if (!other.CompareTag("Interactable")) return;
            _pickupAvailable = false;
        }

        public void SetActiveItem(Data.PlayerItems item)
        {
            DisableAllItems();
            playerItems[(int) item].gameObject.SetActive(true);
        }

        public void RemoveActiveItem()
        {
            DisableAllItems();
        }

        private void DisableAllItems()
        {
            foreach (var item in playerItems)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    
}
