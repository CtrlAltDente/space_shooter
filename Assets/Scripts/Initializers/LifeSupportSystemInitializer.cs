using SpaceShooter.Interfaces;
using SpaceShooter.LifeSupport;
using SpaceShooter.ScriptableObjects;
using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class LifeSupportSystemInitializer : MonoBehaviour, IInitializer
    {
        [SerializeField]
        private LifeSupportSystemsContainer _lifeSupportSystemsContainer;

        [SerializeField]
        private LifeSupportSystem _lifeSupportSystem;

        public void Initialize(UserConfig userConfig)
        {
            InitializeLifeSupportSystem(userConfig.LifeSupportSystemIndex);
        }

        private void InitializeLifeSupportSystem(int lifeSupportSystemIndex)
        {
            _lifeSupportSystem.InitializeLifeSupportSystem(_lifeSupportSystemsContainer.Items[lifeSupportSystemIndex]);
        }
    }
}
