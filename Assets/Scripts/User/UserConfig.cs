using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using SpaceShooter.Base;

namespace SpaceShooter.User
{
    [Serializable]
    public struct UserConfig : INetworkSerializable
    {
        public string Name;

        public int SkinIndex;
        public int GunIndex;
        public int LifeSupportSystemIndex;

        public UserConfig(string name, int skinIndex, int gunIndex, int lifeSupportSystemIndex)
        {
            Name = name;
            
            SkinIndex = skinIndex;
            GunIndex = gunIndex;

            LifeSupportSystemIndex = lifeSupportSystemIndex;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Name);
            serializer.SerializeValue(ref SkinIndex);
            serializer.SerializeValue(ref GunIndex);
            serializer.SerializeValue(ref LifeSupportSystemIndex);
        }
    }
}