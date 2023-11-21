using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceShooter.Player
{
    public struct PlayerData : INetworkSerializable
    {
        public ulong PlayerId;
        public PlayerBodyData PlayerBodyData;
        public PlayerInputData PlayerInputData;

        public PlayerData(ulong playerId, PlayerBodyData playerBodyData, PlayerInputData playerInputData)
        {
            PlayerId = playerId;
            PlayerBodyData = playerBodyData;
            PlayerInputData = playerInputData;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref PlayerId);
            serializer.SerializeValue(ref PlayerBodyData);
            serializer.SerializeValue(ref PlayerInputData);
        }
    }
}