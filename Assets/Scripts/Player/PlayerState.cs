using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerState : NetworkBehaviour
    {
        public NetworkVariable<PlayerData> PlayerData;

        [SerializeField]
        private PlayerSkin _skin;
        [SerializeField]
        private GunsInitializer _playerGunSystem;
        [SerializeField]
        private PlayerHandsInput _playerHandInput;

        public void Start()
        {
            SetPlayerSettingsClientRpc();
        }

        private void Update()
        {
            if(IsServer)
            {
                SetPlayerStatePosition(PlayerData.Value);
            }

            if (!IsOwner)
            {
                _skin.SetBodyData(PlayerData.Value.PlayerBodyData);
            }
        }

        public void SetPlayerData(PlayerData playerData)
        {
            SetLocalPlayerData(playerData);
            SetNetworkPlayerDataServerRpc(playerData);
        }

        [ClientRpc]
        public void SetPlayerSettingsClientRpc()
        {
            _skin.SetSkin(0);
            _playerGunSystem.InitializeGuns(0);
            Debug.Log($"Call setting settings: {NetworkManager.Singleton.LocalClientId}");
        }

        private void SetLocalPlayerData(PlayerData playerData)
        {
            _skin.SetBodyData(playerData.PlayerBodyData);
            _playerHandInput.SetInputData(PlayerData.Value.PlayerInputData);
        }

        [ServerRpc]
        private void SetNetworkPlayerDataServerRpc(PlayerData playerData)
        {
            PlayerData.Value = playerData;
        }

        private void SetPlayerStatePosition(PlayerData playerData)
        {
            transform.position = playerData.PlayerBodyData.Head.Position;
        }
    }
}