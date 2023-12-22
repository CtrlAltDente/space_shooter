using SpaceShooter.Base;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Guns
{
    public class Bullet : NetworkBehaviour
    {
        [SerializeField]
        private BulletOwnerType _type;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private float _movementSpeed = 10;

        [SerializeField]
        private float _damage = 20f;

        [SerializeField]
        private float _destroyTime = 30f;

        private void Start()
        {
            StartMoving();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            StartCoroutine(SelfDestroy());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsHost)
                return;

            Debug.Log(collision.gameObject.name);
            if (collision.transform.GetComponent<IDamagable>() != null)
            {
                collision.gameObject.GetComponent<IDamagable>().TakeDamage(_type, _damage);

                Debug.Log("Damage");
            }

            NetworkObject.Despawn(true);
        }

        public void SetBulletType(BulletOwnerType bulletType)
        {
            _type = bulletType;
        }

        private void StartMoving()
        {
            if (IsOwnedByServer)
            {
                _rigidbody.AddForce(transform.forward * _movementSpeed, ForceMode.Impulse);
            }
        }

        private IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(_destroyTime);
            if (NetworkManager.Singleton.IsHost)
            {
                GetComponent<NetworkObject>().Despawn();
                Destroy(gameObject);
            }
        }
    }
}