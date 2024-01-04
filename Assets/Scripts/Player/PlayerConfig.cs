using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SpaceShooter.Player
{
    [Serializable]
    public struct PlayerConfig : INetworkSerializable
    {
        public string Name;

        public int SkinIndex;
        public int GunIndex;

        public int MaximumHealth;
        public int MaximumShieldEnergy;

        public PlayerConfig(string name, int skinIndex, int gunIndex, int maximumHealth, int maximumShieldEnergy)
        {
            Name = name;
            
            SkinIndex = skinIndex;
            GunIndex = gunIndex;

            MaximumHealth = maximumHealth;
            MaximumShieldEnergy = maximumShieldEnergy;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Name);
            serializer.SerializeValue(ref SkinIndex);
            serializer.SerializeValue(ref GunIndex);
            serializer.SerializeValue(ref MaximumHealth);
            serializer.SerializeValue(ref MaximumShieldEnergy);
        }
    }
}