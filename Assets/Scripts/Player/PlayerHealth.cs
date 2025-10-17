using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth instance;
        [SerializeField] private Data.PlayerHealthStates _currentHealthState;
        private readonly float _healthRecoveryCooldownBase = 2f;
        [SerializeField] private float _healthRecoveryCooldownCurrent = 0f;

        private readonly float _hasBeenHitTimerBase = 3f;
        [SerializeField] private float _hasBeenHitTimerCurrent = 5f;
        private bool _hasBeenHitRecently = false;

        private PlayerHealthEffect _effect;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
            instance = this;
        }

        private void Start()
        {
            _currentHealthState = Data.PlayerHealthStates.FullHealth;
            _effect = GetComponent<PlayerHealthEffect>();
        }

        private void Update()
        {
            if (_hasBeenHitRecently)
            {
                _hasBeenHitTimerCurrent -= Time.deltaTime;
                if (_hasBeenHitTimerCurrent <= 0)
                {
                    _hasBeenHitRecently = false;
                }
            }
            else
            {
                if (_healthRecoveryCooldownCurrent <= 0)
                {
                    HealOneHealthstate();
                    _healthRecoveryCooldownCurrent = _healthRecoveryCooldownBase;
                }
                else
                    _healthRecoveryCooldownCurrent -= Time.deltaTime;
            }
        }

        private void HealOneHealthstate()
        {
            if (_currentHealthState == Data.PlayerHealthStates.FullHealth || 
                _currentHealthState == Data.PlayerHealthStates.Dead) 
                return;
            _currentHealthState--;
        }

        public void DoDamageToPlayer()
        {
            if (_currentHealthState != Data.PlayerHealthStates.Dead)
            {
                _currentHealthState++;
                _effect.OnDamage(_hasBeenHitTimerBase);
                /*
                if(_currentHealthState == Data.PlayerHealthStates.Hit)
                    _effect.OnDamage(_healthRecoveryCooldownBase);
                if(_currentHealthState == Data.PlayerHealthStates.CriticallyWounded)
                    _effect.OnDamage(_healthRecoveryCooldownBase * 2);*/
            }
                
            
            if (_currentHealthState == Data.PlayerHealthStates.Dead)
            {
                // WE DEAD DO SOMETHING
            }
            _hasBeenHitTimerCurrent = _hasBeenHitTimerBase;
            _healthRecoveryCooldownCurrent = _healthRecoveryCooldownBase;
            
            _hasBeenHitRecently = true;
        }
        public void AddGreenVignette() => _effect.ShowGreenVignette(true);

        public void RemoveGreenVignette() => _effect.ShowGreenVignette(false);
    }
}