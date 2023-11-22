using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerState : NetworkBehaviour
    {
        [SerializeField]
        private PlayerSkin _skin;
        [SerializeField]
        private PlayerInteractor _playerInteractor;

        [ClientRpc]
        public void SetDataClientRpc(PlayerData playerData)
        {
            if (playerData.PlayerId == NetworkManager.Singleton.LocalClientId)
                return;
            _skin.SetBodyData(playerData.PlayerBodyData);
            _playerInteractor.SetInputData(playerData.PlayerInputData);
        }

        public void SetDataLocally(PlayerData playerData)
        {
            _skin.SetBodyData(playerData.PlayerBodyData);
            _playerInteractor.SetInputData(playerData.PlayerInputData);
            Debug.Log("Data setted locally");
        }
    }
}