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

        [ServerRpc]
        public void SpawnBulletServerRpc(Vector3 position, Quaternion rotation, Quaternion fireSpread, BulletType bulletType, BulletOwnerType bulletOwnerType, ulong playerId)
        {
            Bullet bulletPrefab = GetBulletByType(bulletType);

            Bullet bullet = Instantiate(bulletPrefab, position, rotation * fireSpread);
            bullet.SetBulletType(bulletOwnerType);
            bullet.GetComponent<NetworkObject>().Spawn();
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
