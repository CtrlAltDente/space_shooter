using SpaceShooter.Base;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Spaceships
{
    public class DestroyerStatus : MonoBehaviour, ITarget
    {
        public bool IsLive => _healthSystem.Health > 0;
        public Transform Transform => transform;

        [SerializeField]
        private HealthSystem _healthSystem;
    }
}