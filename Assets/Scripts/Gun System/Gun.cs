using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Interfaces;
using Unity.Netcode;
using System;

namespace SpaceShooter.Guns
{
    public class Gun : NetworkBehaviour
    {
        [SerializeField]
        private GunSettings _gunSettings;

        [SerializeField]
        protected bool _canShoot = true;

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
                    SpawnBulletServerRpc(spawnPosition.position, spawnPosition.rotation, NetworkManager.Singleton.LocalClientId);
                }
            }
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void SpawnBulletServerRpc(Vector3 position, Quaternion rotation, ulong playerId)
        {
            Quaternion fireSpread = Quaternion.Euler(new Vector3(_gunSettings.RandomFireSpreadValue, _gunSettings.RandomFireSpreadValue, _gunSettings.RandomFireSpreadValue));
            Bullet bullet = Instantiate(_gunSettings.BulletPrefab, position, rotation * fireSpread);
            bullet.SetBulletType(_gunSettings.BulletType);
            bullet.GetComponent<NetworkObject>().Spawn();
        }

        private IEnumerator ShootPause()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_gunSettings.ShootDelay);
            _canShoot = true;
        }
    }

    [Serializable]
    public struct GunSettings
    {
        public Bullet BulletPrefab;
        public float ShootDelay;
        public float FireSpreadDegrees;
        public Transform[] SpawnPositions;
        public int AmmoCount;
        public BulletType BulletType;

        public float RandomFireSpreadValue
        {
            get
            {
                return UnityEngine.Random.Range(0f, FireSpreadDegrees);
            }
        }
    }
}