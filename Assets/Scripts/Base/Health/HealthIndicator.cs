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
        protected PlayerState _playerState;

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
            if (_playerState == null)
                return;

            _healthBar.SetBarValue(_playerState.HealthSystem.Health, _playerState.HealthSystem.BaseHealth.Health);
            _energyBar.SetBarValue(_playerState.HealthSystem.EnergyShield, _playerState.HealthSystem.BaseHealth.EnergyShield);
        }
    }
}