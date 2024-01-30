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
        public float BaseEnergyShield;
        public float BaseHealth;

        public float EnergyShield;
        public float Health;

        [SerializeField]
        private BulletOwnerType _damageFromType;

        [SerializeField]
        private float _restorationSpeed = 2f;

        public UnityEvent OnDestroyed;

        private void Awake()
        {
            Health = BaseHealth;
            EnergyShield = BaseEnergyShield;
        }


        private void Start()
        {
            StartCoroutine(RestoreEnergyShield());
        }

        public void TakeDamage(BulletOwnerType bulletType, float damage)
        {
            if (bulletType != _damageFromType)
                return;

            EnergyShield -= damage;

            if (EnergyShield < 0)
            {
                Health += EnergyShield;
                EnergyShield = 0;
            }

            if (Health < 0)
            {
                OnDestroyed?.Invoke();
            }

            Debug.Log("Damage");
        }

        private IEnumerator RestoreEnergyShield()
        {
            while (Health > 0)
            {
                if (EnergyShield <= BaseEnergyShield)
                {
                    EnergyShield += _restorationSpeed * Time.deltaTime;
                }
                if (EnergyShield >= BaseEnergyShield)
                {
                    EnergyShield = BaseEnergyShield;
                }

                yield return null;
            }
        }
    }
}