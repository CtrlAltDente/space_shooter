using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using SpaceShooter.Base;

namespace SpaceShooter.Player
{
    [Serializable]
    public struct PlayerConfig : INetworkSerializable
    {
        public string Name;

        public int SkinIndex;
        public int GunIndex;

        public SubjectHealth Health;

        public PlayerConfig(string name, int skinIndex, int gunIndex, SubjectHealth subjectHealth)
        {
            Name = name;
            
            SkinIndex = skinIndex;
            GunIndex = gunIndex;

            Health = subjectHealth;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Name);
            serializer.SerializeValue(ref SkinIndex);
            serializer.SerializeValue(ref GunIndex);
            serializer.SerializeValue(ref Health);
        }
    }
}