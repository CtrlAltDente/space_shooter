using SpaceShooter.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.GameLogic
{
    [Serializable]
    public struct SessionConfig : INetworkSerializable
    {
        public SessionType Type;
        public int TypeValue;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Type);
            serializer.SerializeValue(ref TypeValue);
        }

        public SessionConfig(SessionType type, int typeValue)
        {
            Type = type;
            TypeValue = typeValue;
        }
    }
}