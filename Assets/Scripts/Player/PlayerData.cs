using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public struct PlayerData
    {
        public uint PlayerId;
        public PlayerBodyData PlayerBodyData;
        public PlayerInputData PlayerInputData;

        public PlayerData(uint playerId, PlayerBodyData playerBodyData, PlayerInputData playerInputData)
        {
            PlayerId = playerId;
            PlayerBodyData = playerBodyData;
            PlayerInputData = playerInputData;
        }
    }
}