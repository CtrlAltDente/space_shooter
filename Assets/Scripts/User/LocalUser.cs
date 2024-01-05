
using SpaceShooter.GameLogic;
using SpaceShooter.Player;
using SpaceShooter.ScriptableObjects;
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

        [SerializeField]
        private PlayersContainer _playersContainer;

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
                PlayerState playerState = FindPlayerStateInOwnedNetworkObjects();

                if (playerState)
                {
                    _playerState = playerState;

                    SetupPlayerConfig();
                }
            }
        }

        private PlayerState FindPlayerStateInOwnedNetworkObjects()
        {
            List<NetworkObject> clientNetworkObjects = NetworkManager.Singleton.SpawnManager.GetClientOwnedObjects(PlayerId);
            
            if (clientNetworkObjects.Count > 0)
            {
                 return clientNetworkObjects.
                    Find(networkObject => networkObject.
                    GetComponent<PlayerState>() != null).
                    GetComponent<PlayerState>();
            }

            return null;
        }

        private void SetupPlayerConfig()
        {
            PlayerConfig playerConfig = _playersContainer.Items[GameData.PlayerConfigurationIndex];
            playerConfig.Name = GameData.PlayerName;

            _playerState.SetPlayerSettingsClientRpc(playerConfig);
        }
    }
}
