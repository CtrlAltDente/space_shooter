using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.GunSystem
{
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        private GunSettings _gunSettings;

        [SerializeField]
        private bool _canShoot = true;

        public void Shoot()
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
                    Ammo ammo = Instantiate(_gunSettings.AmmoPrefab, spawnPosition.position, spawnPosition.rotation * fireSpread);
                }
            }
        }

        private IEnumerator ShootPause()
        {
            _canShoot = false;
            yield return new WaitForSeconds(60f / _gunSettings.RateOfFire);
            _canShoot = true;
        }
    }

    [System.Serializable]
    public struct GunSettings
    {
        public Ammo AmmoPrefab;
        public int RateOfFire;
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