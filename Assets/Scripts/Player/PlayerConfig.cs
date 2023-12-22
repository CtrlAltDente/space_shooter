using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    [Serializable]
    public struct PlayerConfig
    {
        public string Name;

        public int SkinIndex;
        public int GunIndex;

        public int MaximumHealth;
        public int MaximumenergyShield;
    }
}