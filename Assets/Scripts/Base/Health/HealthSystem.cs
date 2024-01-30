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
        private BulletOwnerType _damageFromType;

        public SubjectHealth CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = BaseHealth;
        }

        private void Start()
        {
            StartCoroutine(RestoreEnergyShield());
        }

        public void TakeDamage(BulletOwnerType bulletType, float damage)
        {
            if (bulletType != _damageFromType)
                return;

            if (CurrentHealth.TakeDamage(damage))
            {
                Debug.Log("Damage");
            }
            else
            {
                OnDestroyed?.Invoke();
            }
        }

        public void RestoreHealth(float health)
        {
            CurrentHealth.RestoreHealth(BaseHealth.Health, health);
        }

        public IEnumerator RestoreEnergyShield()
        {
            while(true)
            {
                CurrentHealth.RestoreEnergyShield(BaseHealth.EnergyShield);

                yield return null;
            }
        }
    }
}