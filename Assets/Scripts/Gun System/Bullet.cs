using SpaceShooter.Base;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Guns
{
    public class Bullet : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [SerializeField]
        private BulletType _type;

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

        private void Update()
        {
            UpdateTransform();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            StartCoroutine(SelfDestroy());
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.transform.GetComponent<IDamagable>() != null)
            {
                collision.gameObject.GetComponent<IDamagable>().TakeDamage(_type, _damage);

                Destroy(gameObject);
                Debug.Log("Damage");
            }
        }

        public void SetBulletType(BulletType bulletType)
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

        private void UpdateTransform()
        {
            if (!IsSpawned)
                return;

            if (IsOwner)
            {
                Position.Value = transform.position;
                Rotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Position.Value;
                transform.rotation = Rotation.Value;
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