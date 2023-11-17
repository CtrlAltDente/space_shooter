
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
        public PlayerBodyData PlayerBodyData => new PlayerBodyData(_playerBodyReference.Head, _playerBodyReference.LeftHand, _playerBodyReference.RightHand);

        public PlayerData PlayerData
        {
            get
            {
                return new PlayerData(PlayerId, PlayerBodyData);
            }
        }

        [SerializeField]
        private PlayerBodyReference _playerBodyReference;
    }
}
