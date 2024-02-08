using SpaceShooter.LifeSupport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User.Configuration
{
    public class LifeSupportSystemDisplayer : ConfigDisplayer<LifeSupportSystemChooser>
    {
        [SerializeField]
        private LifeSupportSystem _lifeSupportSystem;

        public override void DisplayCurrentConfig(int configDataIndex)
        {
            _lifeSupportSystem.InitilizeMesh(_configChooser.Container.Items[configDataIndex]);
        }
    }
}