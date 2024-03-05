using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class GunDisplayer : ConfigDisplayer<GunChooser>
    {
        [SerializeField]
        private Transform _gunParent;

        [SerializeField]
        private Gun _currentGun;

        [SerializeField]
        private TextMeshProUGUI _informationText;

        public override void DisplayCurrentConfig(int configDataIndex)
        {
            DestroyGun(_currentGun);
            _currentGun = Instantiate(_configChooser.Container.Items[configDataIndex].Gun, _gunParent.position, _gunParent.rotation, _gunParent);
            _currentGun.enabled = false;
            ShowInformation();
        }

        private void DestroyGun(Gun gun)
        {
            if (gun != null)
            {
                Destroy(gun.gameObject);
            }
        }

        private void ShowInformation()
        {
            _informationText.text = $"Energy shoot cost: {_currentGun.GunSettings.EnergyCost}\n" +
                $"Damage: {_currentGun.GunSettings.Damage}\n" +
                $"Rate of fire: {1f / _currentGun.GunSettings.ShootDelay:#.##}/second";
        }
    }
}