using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerState : NetworkBehaviour
    {
        public NetworkVariable<PlayerData> PlayerData = new NetworkVariable<PlayerData>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [SerializeField]
        private PlayerSkin _skin;
        [SerializeField]
        private PlayerInteractor _playerInteractor;

        private void Update()
        {
            ApplyPlayerData();
        }

        [ClientRpc]
        public void SetDataClientRpc(PlayerData playerData)
        {
            if (playerData.PlayerId == NetworkManager.Singleton.LocalClientId)
                return;
            _skin.SetBodyData(playerData.PlayerBodyData);
            _playerInteractor.SetInputData(playerData.PlayerInputData);
        }

        public void SetNetworkPlayerData(PlayerData playerData)
        {
            PlayerData.Value = playerData;
        }

        private void ApplyPlayerData()
        {
            _skin.SetBodyData(PlayerData.Value.PlayerBodyData);
            
            if (NetworkManager.Singleton.IsHost)
            {
                _playerInteractor.SetInputData(PlayerData.Value.PlayerInputData);
            }
        }
    }
}