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
    public class OneHandedGun : Gun, IPickableObject, IInteractableObject
    {
        private NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        private NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [SerializeField]
        private Rigidbody _rigidbody;

        private PlayerItemPicker _currentItemPicker;

        private void Update()
        {
            UpdatePositionAndRotation();
        }

        public void Pick(PlayerItemPicker playerItemPicker)
        {
            if (!_currentItemPicker)
            {
                _currentItemPicker = playerItemPicker;
                _rigidbody.isKinematic = true;
                ChangeOwnerServerRpc(playerItemPicker.OwnerClientId);
            }
        }

        public void Drop()
        {
            ChangeOwnerServerRpc(0);
            _currentItemPicker = null;
            transform.parent = null;
            _rigidbody.isKinematic = false;
        }

        [ServerRpc(RequireOwnership = false)]
        public void ChangeOwnerServerRpc(ulong playerId)
        {
            Debug.Log($"Player {playerId}");

            if (playerId == 0)
                playerId = NetworkManager.Singleton.LocalClientId;
            NetworkObject.Despawn(false);
            NetworkObject.SpawnWithOwnership(playerId);

            Debug.Log($"Respawned! Id {playerId}");
        }

        public void Interact()
        {
            Shoot();
        }

        private void UpdatePositionAndRotation()
        {
            if (!IsSpawned)
                return;

            if (IsOwner)
            {
                if (_currentItemPicker)
                {
                    transform.position = _currentItemPicker.transform.position;
                    transform.rotation = _currentItemPicker.transform.rotation;
                }

                Position.Value = transform.position;
                Rotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Position.Value;
                transform.rotation = Rotation.Value;
            }
        }
    }
}