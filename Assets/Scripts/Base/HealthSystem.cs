using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Base
{
    public class HealthSystem : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private float _baseEnergyShield;
        [SerializeField]
        private float _baseHealth;

        public float EnergyShield { get; private set; }
        public float Health { get; private set; }

        public float HealthPercentage
        {
            get
            {
                return Health / _baseHealth;
            }
        }
        public float EnergyShieldPercentage
        {
            get
            {
                return EnergyShield / _baseEnergyShield;
            }
        }

        [SerializeField]
        private float _energyRestorationSpeed = 10f;

        public UnityEvent OnDestroyed;

        private void Awake()
        {
            Health = _baseHealth;
            EnergyShield = _baseEnergyShield;
        }


        private void Start()
        {
            StartCoroutine(RestoreEnergyShield());
        }

        public void TakeDamage(float damage)
        {
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
                if (EnergyShield <= _baseEnergyShield)
                {
                    EnergyShield += _energyRestorationSpeed * Time.deltaTime;
                }

                yield return null;
            }
        }
    }
}