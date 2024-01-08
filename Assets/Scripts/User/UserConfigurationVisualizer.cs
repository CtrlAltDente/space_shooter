using SpaceShooter.Guns;
using SpaceShooter.Player;
using SpaceShooter.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.User
{
    public class UserConfigurationVisualizer : MonoBehaviour
    {
        [SerializeField]
        private PlayersContainer _playersContainer;

        [SerializeField]
        private TextMeshProUGUI _configurationInformationLabel;


        public void ShowContainerInformation(int index)
        {
            PlayerConfig playerConfig = _playersContainer.Items[index];

            _configurationInformationLabel.text =
                $"Health: {playerConfig.MaximumHealth}\n" +
                $"Shield: {playerConfig.MaximumShieldEnergy}\n";
        }
    }
}