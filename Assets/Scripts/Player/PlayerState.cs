using SpaceShooter.Guns;
using SpaceShooter.Initializers;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerState : NetworkBehaviour
    {
        public NetworkVariable<PlayerConfig> PlayerConfig;
        public NetworkVariable<PlayerData> PlayerData;

        [SerializeField]
        private PlayerHandsInput _playerHandInput;
        [SerializeField]
        private PlayerBodyReferences _playerBodyReferences;

        [SerializeField]
        private SkinsInitializer _skinInitializer;
        [SerializeField]
        private GunsInitializer _gunsInitializer;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            SetPlayerSettingsClientRpc(PlayerConfig.Value);
        }

        private void Update()
        {
            if(IsServer)
            {
                SetPlayerStatePosition(PlayerData.Value);
            }

            if (!IsOwner)
            {
                _playerBodyReferences.BodyData = PlayerData.Value.PlayerBodyData;
            }
        }

        public void SetPlayerData(PlayerData playerData)
        {
            SetLocalPlayerData(playerData);
            SetNetworkPlayerDataServerRpc(playerData);
        }

        [ClientRpc]
        public void SetPlayerSettingsClientRpc(PlayerConfig playerConfig)
        {
            PlayerConfig.Value = playerConfig;
            _skinInitializer.InitializeSkin(playerConfig.SkinIndex);
            _gunsInitializer.InitializeGun(playerConfig.GunIndex);
            Debug.Log($"Call setting settings: {NetworkManager.Singleton.LocalClientId}");
        }

        private void SetLocalPlayerData(PlayerData playerData)
        {
            _playerBodyReferences.BodyData = playerData.PlayerBodyData;
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