using SpaceShooter.Base;
using SpaceShooter.Enums;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Guns
{
    public class ShootSystem : NetworkBehaviour
    {
        [SerializeField]
        private BulletContainer[] _bullets;

        [SerializeField]
        private HealthSystem _healthSystem;

        [ServerRpc]
        public void SpawnBulletServerRpc(Vector3 position, Quaternion rotation, Quaternion fireSpread, BulletType bulletType, BulletOwnerType bulletOwnerType, float damage, ulong playerId)
        {
            SpawnBulletClientRpc(position, rotation, fireSpread, bulletType, bulletOwnerType, damage, playerId);
        }

        [ClientRpc]
        public void SpawnBulletClientRpc(Vector3 position, Quaternion rotation, Quaternion fireSpread, BulletType bulletType, BulletOwnerType bulletOwnerType, float damage, ulong playerId)
        {
            if (playerId != NetworkManager.Singleton.LocalClientId)
            {
                SpawnBulletLocally(position, rotation, fireSpread, bulletType, bulletOwnerType, damage);
            }
        }

        public void SpawnBulletLocally(Vector3 position, Quaternion rotation, Quaternion fireSpread, BulletType bulletType, BulletOwnerType bulletOwnerType, float damage)
        {
            Bullet bulletPrefab = GetBulletByType(bulletType);

            Bullet bullet = Instantiate(bulletPrefab, position, rotation * fireSpread);
            bullet.SetBulletType(bulletOwnerType);
            bullet.SetDamage(damage);
        }

        public bool UseEnergy(float neededEnergy)
        {
            bool energyUsedSuccessfully = _healthSystem.UseEnergy(neededEnergy);
            return energyUsedSuccessfully;
        }

        private Bullet GetBulletByType(BulletType bulletType)
        {
            foreach (BulletContainer bulletContainer in _bullets)
            {
                if(bulletContainer.BulletType == bulletType)
                {
                    return bulletContainer.Bullet;
                }
            }

            return null;
        }
    }
}
