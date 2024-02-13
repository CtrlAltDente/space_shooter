using SpaceShooter.Enums;
using SpaceShooter.Extensions;
using SpaceShooter.Interfaces;
using SpaceShooter.LifeSupport;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Base
{
    public class HealthSystem : NetworkBehaviour, IDamagable
    {
        public NetworkVariable<SubjectHealth> NetworkHealth;

        public UnityEvent OnDestroyed;

        [SerializeField]
        private LifeSupportSystem _lifeSupportSystem;
        [SerializeField]
        private bool _useSettedLifeSupportSystem = false;

        [SerializeField]
        private SubjectHealth _currentHealth;

        [SerializeField]
        private BulletOwnerType _damageFromType;

        private Coroutine _shieldRestorationCoroutine;

        public float CurrentEnergyShield => _currentHealth.EnergyShield;
        public float CurrentHealth => _currentHealth.Health;
        public float MaximumHealth => _lifeSupportSystem.Health.Health;
        public float MaximumEnergyShield => _lifeSupportSystem.Health.EnergyShield;

        private void Start()
        {
            if(_useSettedLifeSupportSystem)
            {
                SetHealth(_lifeSupportSystem.Health);
            }
        }

        private void Update()
        {
            if (IsHost)
            {
                NetworkHealth.Value = _currentHealth;
            }
            else if(IsClient)
            {
                _currentHealth = NetworkHealth.Value;
            }
        }

        public void TakeDamage(BulletOwnerType bulletType, float damage)
        {
            if (bulletType != _damageFromType)
                return;

            if (_currentHealth.TakeDamage(damage))
            {
                Debug.Log("Damage");
            }
            else
            {
                OnDestroyed?.Invoke();
            }
        }

        public void SetHealth(SubjectHealth subjectHealth)
        {
            if (!IsHost)
                return;

            this.KillCoroutine(ref _shieldRestorationCoroutine);

            _currentHealth = subjectHealth;

            StartCoroutine(RestoreEnergyShield());
        }

        public bool UseEnergy(float neededEnergy)
        {
            if(_currentHealth.UseEnergy(neededEnergy))
            {
                SetHealthServerRpc(_currentHealth);
                return true;
            } 
            else return false;
        }

        public void RestoreHealth(float health)
        {
            _currentHealth.RestoreHealth(_lifeSupportSystem.Health.Health, health);
        }

        public IEnumerator RestoreEnergyShield()
        {
            while (_currentHealth.Health > 0)
            {
                _currentHealth.RestoreEnergyShield(_lifeSupportSystem.Health.EnergyShield);

                yield return new WaitForEndOfFrame();
            }
        }

        [ServerRpc]
        private void SetHealthServerRpc(SubjectHealth subjectHealth)
        {
            NetworkHealth.Value = subjectHealth;
            _currentHealth = subjectHealth;
        }
    }
}