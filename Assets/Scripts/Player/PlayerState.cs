using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class PlayerState : NetworkBehaviour
    {
        public ulong PlayerId = 0;

        [SerializeField]
        private PlayerSkin _skin;
        [SerializeField]
        private PlayerInteractor _playerInteractor;

        [ClientRpc]
        public void SetDataClientRpc(PlayerData playerData)
        {
            _skin.SetBodyData(playerData.PlayerBodyData);
            _playerInteractor.SetInputData(playerData.PlayerInputData);
        }
    }
}