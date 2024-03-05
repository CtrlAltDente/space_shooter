using SpaceShooter.Base;
using SpaceShooter.Guns;
using SpaceShooter.Initializers;
using SpaceShooter.Interfaces;
using SpaceShooter.User;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerState : NetworkBehaviour
    {
        public NetworkVariable<UserConfig> UserConfig;
        public NetworkVariable<PlayerData> NetworkPlayerData;

        [SerializeField]
        private PlayerHandsInput _playerHandInput;
        [SerializeField]
        private PlayerBodyReferences _playerBodyReferences;

        private PlayerData _localPlayerData;

        public HealthSystem HealthSystem { get; private set; }

        private void Awake()
        {
            HealthSystem = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            InitializeUserConfigClientRpc(UserConfig.Value);
        }

        private void Update()
        {
            if (IsOwner)
            {
                SetNetworkPlayerDataServerRpc(_localPlayerData);
            }
            else
            {
                _playerBodyReferences.BodyData = NetworkPlayerData.Value.PlayerBodyData;
            }
        }

        public void SetPlayerData(PlayerData playerData)
        {
            SetLocalPlayerData(playerData);
        }

        [ServerRpc]
        public void SetUserConfigServerRpc(UserConfig userConfig)
        {
            UserConfig.Value = userConfig;

            InitializeUserConfigClientRpc(UserConfig.Value);
        }

        [ClientRpc]
        public void InitializeUserConfigClientRpc(UserConfig userConfig)
        {
            IInitializer[] initializers = GetComponentsInChildren<IInitializer>();

            foreach(IInitializer initializer in initializers)
            {
                initializer.Initialize(userConfig);
            }
        }

        private void SetLocalPlayerData(PlayerData playerData)
        {
            _playerHandInput.SetInputData(NetworkPlayerData.Value.PlayerInputData);
            _playerBodyReferences.BodyData = playerData.PlayerBodyData;
            _localPlayerData = playerData;
        }

        [ServerRpc]
        private void SetNetworkPlayerDataServerRpc(PlayerData playerData)
        {
            NetworkPlayerData.Value = playerData;
        }
    }
}