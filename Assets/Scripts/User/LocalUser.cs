
using SpaceShooter.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User
{
    public class LocalUser : MonoBehaviour
    {
        public uint PlayerId = 0;
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
