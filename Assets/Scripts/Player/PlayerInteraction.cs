using System.Collections;
using Enemies;
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
        
        // Event 03 Variables
        private bool _fogAvailable = false;
        [CanBeNull] private GameObject _lastFog;
        private uint _fogsCnt = 4;
        
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
            
            // Event 00
            if (EventController.instance.GetActiveEvent() == Data.Events.PlayerPossesed && _input.interact)
            { 
                StartCoroutine(DrinkWine());
            }
            
            // Event 03
            if (_fogAvailable && _input.interact)
            {
                StartCoroutine(CleanseFog());
                _fogAvailable  = false;
            }
            
            // Event 07
            if (_pentagramAvailable && _input.interact)
            {
                StartCoroutine(CleansePentagram());
                _pentagramAvailable = false;
                
            }
        }

        private IEnumerator CleanseFog()
        {
            //GameObject.Find("Senser").GetComponent<Animator>().SetTrigger("Sensing");
            yield return new WaitForSeconds(2f);
            if (_lastFog != null)
            {
                Destroy(_lastFog);
                _fogsCnt--;
                if(_fogsCnt == 0)
                    EventController.instance.StopCurrentEvent();
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

        private IEnumerator DrinkWine()
        {
            //GameObject.Find("ItemTowel").GetComponent<Animator>().SetTrigger("DrinkWine");
            yield return new WaitForSeconds(2f);
            _input.invert = !_input.invert;
            EventController.instance.StopCurrentEvent();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Item Pickup
            if (other.CompareTag("Interactable"))
            {
                _pickupAvailable = true;
            }
            
            // Event 03 
            if (other.CompareTag("Event_03_Fog"))
            {
                _fogAvailable = true;
                _lastFog = other.gameObject;
            }
            
            // Event 07 Pentagram Interact
            if (other.CompareTag("Event_07_Pentagram"))
            {
                _pentagramAvailable = true;
                _lastPentagram = other.gameObject;
            }
            
            // Event 11 Ghost Interact
            if (other.CompareTag("Event_11_Ghost"))
            {
                if(other.GetComponent<Event11Ghosts>().isAttacking)
                    Destroy(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                _pickupAvailable = false;
            }

            if (other.CompareTag("Event_03_Fog"))
            {
                _fogAvailable = false;
                _lastFog = null;
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
