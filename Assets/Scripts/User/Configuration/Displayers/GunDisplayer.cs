using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class GunDisplayer : ConfigDisplayer<GunChooser>
    {
        [SerializeField]
        private Transform _gunParent;

        [SerializeField]
        private Gun _currentGun;

        public override void DisplayCurrentConfig(int configDataIndex)
        {
            DestroyGun(_currentGun);
            _currentGun = Instantiate(_configChooser.Container.Items[configDataIndex].Gun, _gunParent.position, _gunParent.rotation, _gunParent);
            _currentGun.enabled = false;
        }

        private void DestroyGun(Gun gun)
        {
            if (gun != null)
            {
                Destroy(gun.gameObject);
            }
        }
    }
}