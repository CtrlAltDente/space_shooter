
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

        public PlayerState PlayerState => _playerState;

        private void Update()
        {
            DoLocalUserLogic();
        }

        private void DoLocalUserLogic()
        {
            if (_playerState)
            {
                _playerState.SetPlayerData(PlayerData);
            }
            else
            {
                FindPlayerState();
            }
        }

        private void FindPlayerState()
        {
            if (!NetworkManager.Singleton)
                return;

            if (NetworkManager.Singleton.IsClient)
            {
                PlayerState playerState = FindPlayerStateInOwnedNetworkObjects();

                if (playerState)
                {
                    _playerState = playerState;

                    SetupUserConfig();
                }
            }
        }

        private PlayerState FindPlayerStateInOwnedNetworkObjects()
        {
            List<NetworkObject> clientNetworkObjects = NetworkManager.Singleton.SpawnManager.GetClientOwnedObjects(PlayerId);
            
            if (clientNetworkObjects.Count > 0)
            {
                 return clientNetworkObjects.
                    Find(networkObject => networkObject.GetComponent<PlayerState>() != null).
                    GetComponent<PlayerState>();
            }

            return null;
        }

        private void SetupUserConfig()
        {
            UserConfig userConfig = GameData.GameUserConfig;
            
            _playerState.SetUserConfigServerRpc(userConfig);
        }
    }
}
