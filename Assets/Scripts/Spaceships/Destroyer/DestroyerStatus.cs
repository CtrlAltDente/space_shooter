using SpaceShooter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Spaceships
{
    public class DestroyerStatus : MonoBehaviour
    {
        public bool IsLive => _healthSystem.Health > 0;

        [SerializeField]
        private HealthSystem _healthSystem;
    }
}