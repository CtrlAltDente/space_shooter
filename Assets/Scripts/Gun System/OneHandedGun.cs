using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Interfaces;
using SpaceShooter.Player;
using System;
using UnityEngine.UI;
using Unity.Netcode;

namespace SpaceShooter.Guns
{
    public class OneHandedGun : Gun, IPickableItem
    {
        [SerializeField]
        private NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [SerializeField]
        private NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [SerializeField]
        private NetworkVariable<bool> _isPicked = new NetworkVariable<bool>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [SerializeField]
        private Rigidbody _rigidbody;

        private Transform _pickTransform;

        public bool IsPicked => _isPicked.Value;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsServer)
                SetTransformValuesServerRpc(transform.position, transform.rotation);
        }

        private void Start()
        {
            if (!IsServer)
            {
                _rigidbody.isKinematic = true;
            }
        }

        private void Update()
        {
            if (IsPicked)
            {
                if (_pickTransform)
                {
                    transform.position = _pickTransform.position;
                    transform.rotation = _pickTransform.rotation;

                    SetTransformValuesServerRpc(transform.position, transform.rotation);
                }
                else
                {
                    transform.position = Position.Value;
                    transform.rotation = Rotation.Value;
                }
            }
            else if (IsServer)
            {
                SetTransformValuesServerRpc(transform.position, transform.rotation);
            }
            else
            {
                transform.position = Position.Value;
                transform.rotation = Rotation.Value;
            }
        }

        public void Interact()
        {
            Shoot();
        }

        public void Pick(Transform transform)
        {
            if (!_isPicked.Value)
            {
                PickServerRpc();
                _pickTransform = transform;
            }
        }

        public void Drop()
        {
            _pickTransform = null;
            DropServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void PickServerRpc()
        {
            if (IsOwner)
                _isPicked.Value = true;

            _rigidbody.isKinematic = true;
        }

        [ServerRpc(RequireOwnership = false)]
        private void DropServerRpc()
        {
            if (IsOwner)
                _isPicked.Value = false;

            _rigidbody.isKinematic = false;
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetTransformValuesServerRpc(Vector3 position, Quaternion rotation)
        {
            Position.Value = position;
            Rotation.Value = rotation;
        }
    }
}