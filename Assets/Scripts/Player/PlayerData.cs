using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public struct PlayerData
    {
        public uint PlayerId;
        public PlayerBodyData PlayerBodyData;

        public PlayerData(uint playerId, PlayerBodyData playerBodyData)
        {
            PlayerId = playerId;
            PlayerBodyData = playerBodyData;
        }
    }
}