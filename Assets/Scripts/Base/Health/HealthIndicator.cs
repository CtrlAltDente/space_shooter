using SpaceShooter.Player;
using SpaceShooter.UI;
using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField]
        protected HealthSystem _healthSystem;

        [SerializeField]
        private ValueBar _energyBar;
        [SerializeField]
        private ValueBar _healthBar;

        public void Update()
        {
            SetBarsValues();
        }

        private void SetBarsValues()
        {
            if (_healthSystem == null)
                return;

            _healthBar.SetBarValue(_healthSystem.Health, _healthSystem.BaseHealth.Health);
            _energyBar.SetBarValue(_healthSystem.EnergyShield, _healthSystem.BaseHealth.EnergyShield);
        }
    }
}