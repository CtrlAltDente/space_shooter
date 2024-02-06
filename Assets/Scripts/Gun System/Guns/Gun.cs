using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Interfaces;
using Unity.Netcode;
using System;
using SpaceShooter.Player;
using SpaceShooter.Enums;
using SpaceShooter.Skins;

namespace SpaceShooter.Guns
{
    public class Gun : MonoBehaviour, IInteractableObject
    {
        [SerializeField]
        private GunSettings _gunSettings;

        [SerializeField]
        private ShootSystem _shootSystem;

        [SerializeField]
        protected bool _canShoot = true;

        public GameObject GameObject => gameObject;
        public GunSettings GunSettings => _gunSettings;

        public void Interact()
        {
            Shoot();
        }

        public void Shoot()
        {
            if (!_canShoot)
                return;

            if (_shootSystem.UseEnergy(_gunSettings.EnergyCost))
            {
                SpawnBullets();
                StartCoroutine(ShootPause());
            }
        }

        public void SetShootSystem(ShootSystem shootSystem)
        {
            _shootSystem = shootSystem;
        }

        public virtual void SetHands(PlayerHand coreHand, PlayerHand additionalHand = null)
        {

        }

        private void SpawnBullets()
        {
            foreach (Transform spawnPosition in _gunSettings.SpawnPositions)
            {
                for (int i = 0; i < _gunSettings.AmmoCount; i++)
                {
                    SpawnBullet(spawnPosition.position, spawnPosition.rotation, NetworkManager.Singleton.LocalClientId);
                }
            }
        }

        private void SpawnBullet(Vector3 position, Quaternion rotation, ulong playerId)
        {
            Quaternion fireSpread = Quaternion.Euler(new Vector3(_gunSettings.RandomFireSpreadValue, _gunSettings.RandomFireSpreadValue, _gunSettings.RandomFireSpreadValue));
            _shootSystem.SpawnBulletLocally(position, rotation, fireSpread, _gunSettings.BulletType, _gunSettings.BulletOwnerType, _gunSettings.Damage);
            _shootSystem.SpawnBulletServerRpc(position, rotation, fireSpread, _gunSettings.BulletType, _gunSettings.BulletOwnerType, _gunSettings.Damage, playerId);
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
        public BulletType BulletType;
        public float ShootDelay;
        public float EnergyCost;
        public float Damage;
        public float FireSpreadDegrees;
        public Transform[] SpawnPositions;
        public int AmmoCount;
        public BulletOwnerType BulletOwnerType;

        public float RandomFireSpreadValue
        {
            get
            {
                return UnityEngine.Random.Range(0f, FireSpreadDegrees);
            }
        }
    }
}