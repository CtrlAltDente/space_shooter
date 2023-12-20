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
        private PlayerInteractor _playerInteractor;

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

        private void SetLocalPlayerData(PlayerData playerData)
        {
            _skin.SetBodyData(playerData.PlayerBodyData);
            _playerInteractor.SetInputData(PlayerData.Value.PlayerInputData);
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