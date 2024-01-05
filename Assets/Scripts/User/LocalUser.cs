
using SpaceShooter.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.User
{
    public class LocalUser : MonoBehaviour
    {
        [SerializeField]
        private PlayerState _playerState;

        [SerializeField]
        private PlayerBodyReferences _playerBodyReferences;
        [SerializeField]
        private PlayerInputReferences _playerInputReferences;

        public PlayerData PlayerData
        {
            get
            {
                return new PlayerData(PlayerId, PlayerBodyData, PlayerInputData);
            }
        }

        public ulong PlayerId => NetworkManager.Singleton.IsClient ? NetworkManager.Singleton.LocalClientId : 0;

        public PlayerBodyData PlayerBodyData => _playerBodyReferences.BodyData;
        public PlayerInputData PlayerInputData => _playerInputReferences.InputData;

        private void Update()
        {
            if (_playerState)
            {
                _playerState.SetPlayerData(PlayerData);
            }
            else
            {
                GetPlayerState();
            }
        }

        private void GetPlayerState()
        {
            if (!NetworkManager.Singleton)
                return;

            if (NetworkManager.Singleton.IsClient)
            {
                List<NetworkObject> clientNetworkObjects = NetworkManager.Singleton.SpawnManager.GetClientOwnedObjects(PlayerId);

                if (clientNetworkObjects.Count > 0)
                {
                    PlayerState playerState = clientNetworkObjects.
                        Find(networkObject => networkObject.
                        GetComponent<PlayerState>() != null).
                        GetComponent<PlayerState>();

                    if (playerState)
                    {
                        _playerState = playerState;

                        _playerState.SetPlayerSettingsClientRpc(new PlayerConfig($"Player {PlayerId}", 0, 1, 50, 50));
                    }
                }
            }
        }
    }
}
