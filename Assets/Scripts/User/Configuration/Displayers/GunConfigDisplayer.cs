using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class GunConfigDisplayer : ConfigDisplayer<GunChooser>
    {
        [SerializeField]
        private Transform _gunTransfrom;

        [SerializeField]
        private Gun _currentGun;

        public override void DisplayCurrentConfig(int configDataIndex)
        {
            DestroyGun(_currentGun);
            _currentGun = Instantiate(_configChooser.Container.Items[configDataIndex].Gun, new Vector3(0, 0, 0), Quaternion.identity, _gunTransfrom);
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