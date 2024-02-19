using SpaceShooter.Data;
using SpaceShooter.Skins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class SkinChooser : ConfigChooser<Skin>
    {
        public override int GameDataConfigIndex
        {
            get => GameData.SkinIndex;
            set => GameData.SkinIndex = value;
        }
    }
}