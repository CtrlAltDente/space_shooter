using SpaceShooter.Enums;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Base
{
    public class HealthSystem : MonoBehaviour, IDamagable
    {
        public UnityEvent OnDestroyed;

        public SubjectHealth BaseHealth;

        [SerializeField]
        private SubjectHealth _currentHealth;

        [SerializeField]
        private BulletOwnerType _damageFromType;

        public float EnergyShield => _currentHealth.EnergyShield;
        public float Health => _currentHealth.Health;

        private void Awake()
        {
            _currentHealth = BaseHealth;
        }

        private void Start()
        {
            StartCoroutine(RestoreEnergyShield());
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

        public bool UseEnergy(float neededEnergy)
        {
            return _currentHealth.UseEnergy(neededEnergy);
        }

        public void RestoreHealth(float health)
        {
            _currentHealth.RestoreHealth(BaseHealth.Health, health);
        }

        public IEnumerator RestoreEnergyShield()
        {
            while (_currentHealth.Health > 0)
            {
                _currentHealth.RestoreEnergyShield(BaseHealth.EnergyShield);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}