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

        [SerializeField]
        private GunsPreset[] _gunsPresets;
        [SerializeField]
        private SkinPreset[] _skinsPresets;

        public void ShowContainerInformation(int index)
        {
            PlayerConfig playerConfig = _playersContainer.Items[index];

            _configurationInformationLabel.text =
                $"Health: {playerConfig.MaximumHealth}\n" +
                $"Shield: {playerConfig.MaximumShieldEnergy}\n";

            ResetGuns();
            _gunsPresets[playerConfig.GunIndex].SetActiveGuns(true);

            ResetSkin();
            _skinsPresets[playerConfig.SkinIndex].SetActiveSkin(true);
        }

        private void ResetGuns()
        {
            foreach(GunsPreset gunsPreset in _gunsPresets)
            {
                gunsPreset.SetActiveGuns(false);
            }
        }

        private void ResetSkin()
        {
            foreach(SkinPreset skinPreset in _skinsPresets)
            {
                skinPreset.SetActiveSkin(false);
            }
        }
    }
}