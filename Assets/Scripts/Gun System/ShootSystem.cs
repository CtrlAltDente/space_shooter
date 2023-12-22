using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Guns
{
    public class ShootSystem : NetworkBehaviour
    {
        [SerializeField]
        private Bullet _bulletPrefab;

        public void SetBulletPrefab(Bullet bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
        }

        [ServerRpc]
        public void SpawnBulletServerRpc(Vector3 position, Quaternion rotation, Quaternion fireSpread, ulong playerId)
        {
            Bullet bullet = Instantiate(_bulletPrefab, position, rotation * fireSpread);
            bullet.SetBulletType(BulletType.PlayerBullet);
            bullet.GetComponent<NetworkObject>().Spawn();
        }
    }
}
