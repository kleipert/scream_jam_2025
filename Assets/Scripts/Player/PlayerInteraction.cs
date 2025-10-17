using System;
using System.Collections;
using Enemies;
using Events;
using GameManager;
using JetBrains.Annotations;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] playerItems;
        [SerializeField] private GameObject mainCamera;
        [Tooltip("Pick-Up")] [SerializeField] private AudioClip audioClip;
        [Tooltip("Event00")] [SerializeField] private AudioClip audioClip00;
        [Tooltip("Event03")] [SerializeField] private AudioClip audioClip03;
        [Tooltip("Event04")] [SerializeField] private AudioClip audioClip04;
        [Tooltip("Event07")] [SerializeField] private AudioClip audioClip07;
        [Tooltip("Event08")] [SerializeField] private AudioClip audioClip08;
        [Tooltip("Event12")] [SerializeField] private AudioClip audioClip12;
        
        private StarterAssetsInputs _input;
        private bool _pickupAvailable = false;
        
        // Event_00 Variable
        private bool _canDrink;
        private bool _endEvent00;
        
        // Event 02 Variables
        [SerializeField] private DialogueRunner dialogueRunner;
        [SerializeField] private GameObject event02;
        private bool _phoneAvailable = false;
        
        // Event 03 Variables
        private bool _fogAvailable = false;
        [CanBeNull] private GameObject _lastFog;
        private uint _fogsCnt = 4;
        
        // Event 04 Variables
        [SerializeField] private GameObject cursedObject;
        private bool _curseAvaible;
        
        // Event 05 Variables 
        [SerializeField] private GameObject possessedWoman;
        [SerializeField] private float flyingSpeed = 1.0f;
        [SerializeField] private float height = 2.0f;
        private bool _isFlying;
        private Vector3 _startPos;
        private float _maxHeight;
        private AudioSource _audioSource;
        
        // Event 07 Variables
        private bool _pentagramAvailable = false;
        [CanBeNull] private GameObject _lastPentagram;
        private uint pentagramsCnt = 5;
        
        // Event 08 Variables
        [SerializeField] private GameObject[] circles;
        private uint _circlesCnt;
        private bool _isWriting;
        private bool _endEvent08 = true;
        
        // Event 09 Variables
        [SerializeField] private GameObject sink;
        private bool _inSinkRange = false;
        private bool _isHitByLaser = false;
        
        // Event 10 Variables
        private bool _inTieDownRangeLeft = false;
        private bool _inTieDownRangeRight = false;
        private bool _leftTied;
        private bool _rightTied;
        
        // Event 12 Variables 
        private bool _canHeal;
        private bool _isHealing;
        
        // Event 13 Variables
        private bool _inTVRange = false;
        
        // Event 14 Variables
        [SerializeField] private GameObject holyWaterPrefab;
        [SerializeField] private GameObject holyWaterThrowPos;
        private readonly float _holyWaterThrowCooldownBase = 2f;
        private float _holyWaterThrowCooldownCurrent = 2f;
        
        
        void Awake()
        {
            if (!dialogueRunner) dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        }

        private void Start()
        {
            _startPos = possessedWoman.transform.position;
            _maxHeight = possessedWoman.transform.position.y + height;
            _input = GetComponentInParent<StarterAssetsInputs>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            // Item Pickup
            if (_pickupAvailable && _input.interact)
            {
                PlayerItemController.instance.GetCorrectPlayerItem();
                _pickupAvailable = false;
                E_Button.Instance.HideButton();
            }
            
            // Event 00
            if (EventController.instance.GetActiveEvent() == Data.Events.PlayerPossesed && _input.interact)
            {
                StartCoroutine(WaitForWine());
                if (_canDrink && !_endEvent00)
                {
                    _endEvent00 = true;
                    StartCoroutine(DrinkWine());
                }
            }
            
            if (EventController.instance.GetActiveEvent() == Data.Events.PlayerPossesed && PlayerItemController.instance.hasItem)
                E_Button.Instance.ShowButton();
            
            //Event 02
            if (_phoneAvailable && _input.interact)
            {
                _phoneAvailable = false;
                if (!dialogueRunner.IsDialogueRunning)
                {
                    event02.GetComponent<Event_02_PhoneRings>().StopAudio();
                    dialogueRunner.StartDialogue("Dialog_Phone");
                    EventController.instance.StopCurrentEvent();
                }
            }
            
            // Event 03
            if (_fogAvailable && _input.interact)
            {
                StartCoroutine(CleanseFog());
                _fogAvailable  = false;
            }
            
            // Event 04
            if (_curseAvaible && _input.interact)
            {
                _curseAvaible = false;
                cursedObject.SetActive(false);
                SoundManager.Instance.PlaySound(audioClip04,transform,0.5f);
                EventController.instance.StopCurrentEvent();
            }
            
            // Event 05
            if (EventController.instance.GetActiveEvent() == Data.Events.DemonFlies)
            {
                if (!_isFlying)
                    StartCoroutine(StartFlying());
                if (_isFlying && possessedWoman.transform.position.y < _maxHeight)
                    possessedWoman.transform.position += Vector3.up * (flyingSpeed * Time.deltaTime);
                if (possessedWoman.transform.position.y < _startPos.y)
                {
                    EventController.instance.StopCurrentEvent();
                    _audioSource.Stop();
                }
            }
            
            // Event 07
            if (_pentagramAvailable && _input.interact)
            {
                StartCoroutine(CleansePentagram());
                _pentagramAvailable = false;
            }
            
            // Event 08
            if (EventController.instance.GetActiveEvent() == Data.Events.PortalOpens)
            {
                foreach (var circle in circles)
                {
                    circle.SetActive(true);
                }
                
                if (_circlesCnt == 4 && _endEvent08)
                {
                    StartCoroutine(EndCircle());
                    _endEvent08 = false;
                }    
            }
            
            // Event 09
            if ((EventController.instance.GetActiveEvent() == Data.Events.DemonThrowsUp ||
                 EventController.instance.lastEvent == Data.Events.DemonThrowsUp) && 
                _input.interact && 
                _inSinkRange &&
                _isHitByLaser)
            {
                PlayerHealth.instance.RemoveGreenVignette();
                _isHitByLaser = false;
            }
            
            // Event 10
            if (_inTieDownRangeLeft && _input.interact && !_leftTied)
            {
                GameObject.Find("Event10").GetComponent<Event_10_TieDemonDown>().TieDownLeft();
                _leftTied = true;
                E_Button.Instance.HideButton();
            }
            
            // Event 10
            if (_inTieDownRangeRight && _input.interact && !_rightTied)
            {
                GameObject.Find("Event10").GetComponent<Event_10_TieDemonDown>().TieDownRight();
                _rightTied = true;
                E_Button.Instance.HideButton();
            }
            
            // Event 12
            if (EventController.instance.GetActiveEvent() == Data.Events.FirstAidForDemon)
            {
                if (_canHeal && _input.interact && PlayerItemController.instance.hasItem && !_isHealing)
                {
                    StartCoroutine(HealDemon());
                    _isHealing = true;
                }
            }
            
            // Event 13
            if (_inTVRange && 
                EventController.instance.GetActiveEvent() == Data.Events.TelevisionTurnsOn && 
                _input.interact)
            {
                GameObject.Find("Event13")?.GetComponent<Event_13_TelevisionTurnsOn>()?.TurnOffTV();
            }
            
            // Event 14
            if (EventController.instance.GetActiveEvent() == Data.Events.SatanistsAttack &&
                PlayerItemController.instance.hasItem &&
                _input.interact &&
                _holyWaterThrowCooldownCurrent <= 0)
            {
                _holyWaterThrowCooldownCurrent = _holyWaterThrowCooldownBase;
                ThrowHolyWater();
            }

            _holyWaterThrowCooldownCurrent -= Time.deltaTime;
        }

        private void ThrowHolyWater()
        {
            var holyWater = Instantiate(holyWaterPrefab, holyWaterThrowPos.transform.position, holyWaterThrowPos.transform.rotation);
        }

        private IEnumerator CleanseFog()
        {
            //GameObject.Find("Senser").GetComponent<Animator>().SetTrigger("Sensing");
            SoundManager.Instance.PlaySound(audioClip03,transform,0.5f,2f);
            yield return new WaitForSeconds(2f);
            E_Button.Instance.HideButton();
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
            SoundManager.Instance.PlaySound(audioClip07,transform,0.3f);
            yield return new WaitForSeconds(2f);
            if (_lastPentagram != null)
            {
                Destroy(_lastPentagram);
                pentagramsCnt--;
                if(pentagramsCnt == 0)
                    EventController.instance.StopCurrentEvent();
            }
        }

        private IEnumerator WaitForWine()
        {
            yield return new WaitForSeconds(1f);
            _canDrink = true;
        }

        private IEnumerator DrinkWine()
        {
            //GameObject.Find("ItemTowel").GetComponent<Animator>().SetTrigger("DrinkWine");
            SoundManager.Instance.PlaySound(audioClip00,transform,0.5f);
            yield return new WaitForSeconds(2f);
            _input.invert = false;
            EventController.instance.StopCurrentEvent();
        }

        private IEnumerator StartFlying()
        {
            yield return new WaitForSeconds(2f);
            _isFlying = true;
        }

        private IEnumerator EndCircle()
        {
            yield return new WaitForSeconds(2f);
            
            foreach (var circle in circles)
            {
                for (int i = 0; i < circle.transform.childCount; i++)
                {
                    circle.transform.GetChild(i).gameObject.SetActive(false);
                }
                
                circle.SetActive(false);
            }
            
            EventController.instance.StopCurrentEvent();
        }

        private IEnumerator HealDemon()
        {
            SoundManager.Instance.PlaySound(audioClip12,transform,0.1f);
            yield return new WaitForSeconds(2f);
            EventController.instance.StopCurrentEvent();
        }

        private IEnumerator WriteRune(GameObject other)
        {
            SoundManager.Instance.PlaySound(audioClip08,transform,0.6f);
            _isWriting = true;
            yield return new WaitForSeconds(2f);
            other.SetActive(true);
            _isWriting = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Item Pickup
            if (other.CompareTag("Interactable"))
            {
                _pickupAvailable = true;
                if (EventController.instance.GetActiveItem() != Data.PlayerItems.None && !PlayerItemController.instance.hasItem)
                    E_Button.Instance.ShowButton();
            }
            
            //Event 02
            if (other.CompareTag("Event_02_Phone") && !dialogueRunner.IsDialogueRunning)
            {
                _phoneAvailable = true;
                E_Button.Instance.ShowButton();
            }
            
            // Event 03 
            if (other.CompareTag("Event_03_Fog"))
            {
                _fogAvailable = true;
                _lastFog = other.gameObject;
                E_Button.Instance.ShowButton();
            }
            
            // Event 04
            if (other.CompareTag("Event_04_Cursed"))
            {
                _curseAvaible = true;
                E_Button.Instance.ShowButton();
            }
            
            // Event 07 Pentagram Interact
            if (other.CompareTag("Event_07_Pentagram"))
            {
                _pentagramAvailable = true;
                _lastPentagram = other.gameObject;
                E_Button.Instance.ShowButton();
            }
            
            // Event 09 Sink
            if (other.CompareTag("Event_09_Sink") && 
                (EventController.instance.GetActiveEvent() == Data.Events.DemonThrowsUp ||
                 EventController.instance.lastEvent == Data.Events.DemonThrowsUp)
                && _isHitByLaser)
            {
                _inSinkRange = true;
                E_Button.Instance.ShowButton();
            }
            
            // Event 09 Laser
            if (other.gameObject.CompareTag("Event_09_LaserBeam") && EventController.instance.GetActiveEvent() == Data.Events.DemonThrowsUp)
            {
                _isHitByLaser = true;
                if(_isHitByLaser)
                    PlayerHealth.instance.AddGreenVignette();
            }
            
            // Event 10 
            if (other.CompareTag("Event_10_BedAnchorLeft"))
            {
                _inTieDownRangeLeft = true;
                E_Button.Instance.ShowButton();
            }
            
            // Event 10 
            if (other.CompareTag("Event_10_BedAnchorRight"))
            {
                _inTieDownRangeRight = true;
                E_Button.Instance.ShowButton();
            }
            
            // Event 11 Ghost Interact
            if (other.CompareTag("Event_11_Ghost"))
            {
                if(other.GetComponent<Event11Ghosts>().isAttacking)
                    Destroy(other.gameObject);
            }
            
            // Event 12 
            if (other.CompareTag("Event_12_Heal"))
            {
                _canHeal = true;
                E_Button.Instance.ShowButton();
            }
            
            // Event 13
            if (other.CompareTag("Event_13_FlickerTV") && EventController.instance.GetActiveEvent() == Data.Events.TelevisionTurnsOn)
            {
                _inTVRange = true;
                E_Button.Instance.ShowButton();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                _pickupAvailable = false;
                E_Button.Instance.HideButton();
            }

            if (other.CompareTag("Event_02_Phone"))
            {
                E_Button.Instance.HideButton();
            }

            if (other.CompareTag("Event_03_Fog"))
            {
                _fogAvailable = false;
                _lastFog = null;
                E_Button.Instance.HideButton();
            }    
            
            if (other.CompareTag("Event_04_Cursed"))
                E_Button.Instance.HideButton();
            
            if (other.CompareTag("Event_07_Pentagram"))
            {
                _pentagramAvailable = false;
                _lastPentagram = null;
                E_Button.Instance.HideButton();
            }
            
            if (other.CompareTag("Event_08_Circle"))
                E_Button.Instance.HideButton();
            
            // Event 09 Sink Interact
            if (other.CompareTag("Event_09_Sink") &&
                (EventController.instance.GetActiveEvent() == Data.Events.DemonThrowsUp ||
                 EventController.instance.lastEvent == Data.Events.DemonThrowsUp))
            {
                _inSinkRange = false;
                E_Button.Instance.HideButton();
            }
            
            // Event 10
            if (other.CompareTag("Event_10_BedAnchorLeft"))
            {
                _inTieDownRangeLeft = false;
                E_Button.Instance.HideButton();
            }
            
            // Event 10 
            if (other.CompareTag("Event_10_BedAnchorRight"))
            {
                _inTieDownRangeRight = false;
                E_Button.Instance.HideButton();
            }
            
            // Event 12 
            if (other.CompareTag("Event_12_Heal"))
            {
                _canHeal = false;
                E_Button.Instance.HideButton();
            }
            
            if (other.CompareTag("Event_13_FlickerTV") && EventController.instance.GetActiveEvent() == Data.Events.TelevisionTurnsOn)
            {
                _inTVRange = false;
                E_Button.Instance.HideButton();
            }

            if (other.CompareTag("Event_05_Flying"))
            {
                E_Button.Instance.HideButton();
            }

            if (other.CompareTag("Event_08_Circle"))
            {
                E_Button.Instance.HideButton();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            // Event 05
            if (other.CompareTag("Event_05_Flying"))
            {
                if (_input.interact && PlayerItemController.instance.hasItem)
                {
                    possessedWoman.transform.position += Vector3.down * (flyingSpeed * 1.3f * Time.deltaTime);
                    if (!_audioSource.isPlaying)
                        _audioSource.Play();
                }
                else
                {
                    if (_audioSource.isPlaying) _audioSource.Stop();
                }
                E_Button.Instance.ShowButton();
            }
            
            // Event 08
            if (other.CompareTag("Event_08_Circle"))
            {
                if (_input.interact && PlayerItemController.instance.hasItem && !_isWriting)
                {
                    for (int i = 0; i < other.transform.childCount; i++)
                    {
                        StartCoroutine(WriteRune(other.transform.GetChild(i).gameObject));
                    }
                }
                E_Button.Instance.ShowButton();
            }
        }

        public void SetActiveItem(Data.PlayerItems item)
        {
            DisableAllItems();
            if (item != Data.PlayerItems.None)
            {
                playerItems[(int) item].gameObject.SetActive(true);
                SoundManager.Instance.PlaySound(audioClip,transform,0.4f);
            }
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

        public void CircleCntIncrease()
        {
            _circlesCnt++;
        }
    }
}
