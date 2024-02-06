using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.UI
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField]
        private LocalUser _localUser;

        [SerializeField]
        private ValueBar _healthBar;
        [SerializeField]
        private ValueBar _energyBar;

        public void Update()
        {
            SetBarsValues();
        }

        private void SetBarsValues()
        {
            if (_localUser.PlayerState == null)
                return;

            _healthBar.SetBarValue(_localUser.PlayerState.HealthSystem.Health, _localUser.PlayerState.HealthSystem.BaseHealth.Health);
            _energyBar.SetBarValue(_localUser.PlayerState.HealthSystem.EnergyShield, _localUser.PlayerState.HealthSystem.BaseHealth.Health);
        }
    }
}