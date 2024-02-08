using SpaceShooter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.LifeSupport
{
    public class LifeSupportSystem : MonoBehaviour
    {
        [SerializeField]
        private SubjectHealth _health;

        [SerializeField]
        private MeshFilter _meshFilter;
        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        private HealthSystem _healthSystem;

        public SubjectHealth Health => _health;
        public MeshFilter MeshFilter => _meshFilter;
        public MeshRenderer MeshRenderer => _meshRenderer;

        public void InitializeLifeSupportSystem(LifeSupportSystem lifeSupportSystem)
        {
            InitilizeMesh(lifeSupportSystem);
            InitializeHealth(lifeSupportSystem);
        }

        public void InitilizeMesh(LifeSupportSystem lifeSupportSystem)
        {
            _meshFilter.sharedMesh = lifeSupportSystem.MeshFilter.sharedMesh;
            _meshRenderer.sharedMaterials = lifeSupportSystem.MeshRenderer.sharedMaterials;
        }

        public void InitializeHealth(LifeSupportSystem lifeSupportSystem)
        {
            _health = lifeSupportSystem.Health;
            _healthSystem.SetHealth(_health);
        }
    }
}