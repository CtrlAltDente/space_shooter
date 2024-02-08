using SpaceShooter.LifeSupport;
using SpaceShooter.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class LifeSupportSystemInitializer : MonoBehaviour
    {
        [SerializeField]
        private LifeSupportSystemsContainer _lifeSupportSystemsContainer;

        [SerializeField]
        private LifeSupportSystem _lifeSupportSystem;

        public void InitializeLifeSupportSystem(int lifeSupportSystemIndex)
        {
            _lifeSupportSystem.InitializeLifeSupportSystem(_lifeSupportSystemsContainer.Items[lifeSupportSystemIndex]);
        }
    }
}
