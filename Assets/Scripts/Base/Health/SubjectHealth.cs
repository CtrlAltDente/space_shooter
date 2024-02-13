using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.Base
{
    [Serializable]
    public struct SubjectHealth : INetworkSerializable
    {
        public float EnergyShield;
        public float Health;

        public float EnergyRestorationSpeed;

        public SubjectHealth(float energyShield, float health, float restorationSpeed)
        {
            EnergyShield = energyShield;
            Health = health;
            EnergyRestorationSpeed = restorationSpeed;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref EnergyShield);
            serializer.SerializeValue(ref Health);
            serializer.SerializeValue(ref EnergyRestorationSpeed);
        }

        public bool TakeDamage(float damage)
        {
            EnergyShield -= damage;

            if (EnergyShield < 0)
            {
                Health += EnergyShield;
                EnergyShield = 0;
            }

            bool isAliveAfterDamage = Health > 0;
            return isAliveAfterDamage;
        }

        public void RestoreEnergyShield(float maxEnergyShield)
        {
            EnergyShield = EnergyShield <= maxEnergyShield ? EnergyShield + EnergyRestorationSpeed * Time.deltaTime : EnergyShield;
            EnergyShield = EnergyShield > maxEnergyShield ? maxEnergyShield : EnergyShield;
        }

        public void RestoreHealth(float maxHealth, float health)
        {
            Health += health;
            Health = Health > maxHealth ? maxHealth : Health;
        }

        public bool UseEnergy(float neededEnergy)
        {
            if (EnergyShield > neededEnergy)
            {
                Debug.Log($"Energy should be: {EnergyShield - neededEnergy}");
                EnergyShield -= neededEnergy;
                Debug.Log($"Energy: {EnergyShield}");

                return true;
            }

            return false;
        }
    }
}