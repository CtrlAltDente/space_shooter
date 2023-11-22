using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Interfaces;
using Unity.Netcode;

namespace SpaceShooter.Guns
{
    public class Gun : NetworkBehaviour
    {
        [SerializeField]
        private GunSettings _gunSettings;

        [SerializeField]
        private bool _canShoot = true;

        protected void Shoot()
        {
            if (!_canShoot)
                return;

            SpawnBullets();
            StartCoroutine(ShootPause());
        }

        private void SpawnBullets()
        {
            foreach (Transform spawnPosition in _gunSettings.SpawnPositions)
            {
                for (int i = 0; i < _gunSettings.AmmoCount; i++)
                {
                    Quaternion fireSpread = Quaternion.Euler(new Vector3(_gunSettings.RandomFireSpreadValue, _gunSettings.RandomFireSpreadValue, _gunSettings.RandomFireSpreadValue));
                    Bullet ammo = Instantiate(_gunSettings.AmmoPrefab, spawnPosition.position, spawnPosition.rotation * fireSpread);
                }
            }
        }

        private IEnumerator ShootPause()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_gunSettings.ShootDelay);
            _canShoot = true;
        }
    }

    [System.Serializable]
    public struct GunSettings
    {
        public Bullet AmmoPrefab;
        public float ShootDelay;
        public float FireSpreadDegrees;
        public Transform[] SpawnPositions;
        public int AmmoCount;

        public float RandomFireSpreadValue
        {
            get
            {
                return Random.Range(0f, FireSpreadDegrees);
            }
        }
    }
}