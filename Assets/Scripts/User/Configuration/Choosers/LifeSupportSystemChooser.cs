using SpaceShooter.GameLogic;
using SpaceShooter.LifeSupport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class LifeSupportSystemChooser : ConfigChooser<LifeSupportSystem>
    {
        public override int GameDataConfigIndex
        { 
            get => GameData.LifeSupportSystemIndex;
            set => GameData.LifeSupportSystemIndex = value;
        }


    }
}