using System.Collections;
using GameManager;
using JetBrains.Annotations;
using StarterAssets;
using UnityEngine;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] playerItems;
        private StarterAssetsInputs _input;
        private bool _pickupAvailable = false;
        
        
        // Event 07 Variables
        private bool _pentagramAvailable = false;
        [CanBeNull] private GameObject _lastPentagram;
        private uint pentagramsCnt = 5;

        private void Start()
        {
            _input = GetComponentInParent<StarterAssetsInputs>();
        }

        private void Update()
        {
            // Item Pickup
            if (_pickupAvailable && _input.interact)
            {
                PlayerItemController.instance.GetCorrectPlayerItem();
                _pickupAvailable = false;
            }
            
            // Event 07
            if (_pentagramAvailable && _input.interact)
            {
                StartCoroutine(CleansePentagram());
                _pentagramAvailable = false;
                
            }
        }

        private IEnumerator CleansePentagram()
        {
            GameObject.Find("ItemTowel").GetComponent<Animator>().SetTrigger("TowelCleaning");
            yield return new WaitForSeconds(2f);
            if (_lastPentagram != null)
            {
                Destroy(_lastPentagram);
                pentagramsCnt--;
                if(pentagramsCnt == 0)
                    EventController.instance.StopCurrentEvent();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Item Pickup
            if (other.CompareTag("Interactable"))
            {
                _pickupAvailable = true;
            }
            
            // Event 07 Pentagram Interact
            if (other.CompareTag("Event_07_Pentagram"))
            {
                _pentagramAvailable = true;
                _lastPentagram = other.gameObject;
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                _pickupAvailable = false;
            }
            
            if (other.CompareTag("Event_07_Pentagram"))
            {
                _pentagramAvailable = false;
                _lastPentagram = null;
            }
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
