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
        private float _movementSpeed = 50f;

        [SerializeField]
        private float _damage = 20f;

        [SerializeField]
        private float _destroyTime = 30f;

        private void Update()
        {
            UpdateTransform();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            StartCoroutine(SelfDestroy());
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (other.transform.GetComponent<IDamagable>() != null)
            {
                other.gameObject.GetComponent<IDamagable>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }

        private void UpdateTransform()
        {
            Debug.Log($"IsSpawned {IsSpawned}, IsOwner {IsOwner}");
            Debug.Log($"Position {Position.Value}, Rotation {Rotation.Value}");

            if (!IsSpawned)
                return;

            if (IsOwner)
            {
                transform.position += transform.forward * _movementSpeed * Time.deltaTime;

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