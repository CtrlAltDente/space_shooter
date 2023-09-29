using SpaceShooter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI.Destroyer
{
    public class HealthPanel : MonoBehaviour
    {
        [SerializeField]
        private HealthSystem _healthSystem;

        [SerializeField]
        private PercentageBar _healthBar;
        [SerializeField]
        private PercentageBar _energyShieldBar;

        private void Update()
        {
            UpdateBars();
        }

        private void UpdateBars()
        {
            _healthBar.Percentage = _healthSystem.HealthPercentage;
            _energyShieldBar.Percentage = _healthSystem.EnergyShieldPercentage;
        }
    }
}