
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
        public ulong PlayerId => NetworkManager.Singleton.IsClient ? NetworkManager.Singleton.LocalClientId : 0;
        public PlayerBodyData PlayerBodyData => new PlayerBodyData(_playerBodyReferences.Head, _playerBodyReferences.LeftHand, _playerBodyReferences.RightHand);
        public PlayerInputData PlayerInputData => new PlayerInputData(_playerInputReferences.LeftTriggerPressed, _playerInputReferences.RightTriggerPressed);

        public PlayerData PlayerData
        {
            get
            {
                return new PlayerData(PlayerId, PlayerBodyData, PlayerInputData);
            }
        }

        [SerializeField]
        private PlayerBodyReferences _playerBodyReferences;
        [SerializeField]
        private PlayerInputReferences _playerInputReferences;
    }
}
