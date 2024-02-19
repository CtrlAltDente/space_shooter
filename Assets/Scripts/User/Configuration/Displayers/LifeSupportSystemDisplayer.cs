using SpaceShooter.LifeSupport;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class LifeSupportSystemDisplayer : ConfigDisplayer<LifeSupportSystemChooser>
    {
        [SerializeField]
        private LifeSupportSystem _lifeSupportSystem;

        [SerializeField]
        private TextMeshProUGUI _informationText;

        public override void DisplayCurrentConfig(int configDataIndex)
        {
            _lifeSupportSystem.InitilizeMesh(_configChooser.Container.Items[configDataIndex]);
            ShowInformation(_configChooser.Container.Items[configDataIndex]);
        }

        private void ShowInformation(LifeSupportSystem lifeSupportSystem)
        {
            _informationText.text = $"Health: {lifeSupportSystem.Health.Health}\n" +
                $"Energy shield: {lifeSupportSystem.Health.EnergyShield}\n" +
                $"Shield restoration speed: {lifeSupportSystem.Health.EnergyRestorationSpeed}/second";
        }
    }
}