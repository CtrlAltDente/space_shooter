using SpaceShooter.GameLogic;
using SpaceShooter.Guns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class GunChooser : ConfigChooser<Gun>
    {
        public override int GameDataConfigIndex 
        {
            get => GameData.GunIndex; 
            set => GameData.GunIndex = value; 
        }
    }
}