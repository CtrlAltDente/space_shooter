using SpaceShooter.Interfaces;
using SpaceShooter.Spaceships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.UI.Destroyer
{
    public class RadarTarget : MonoBehaviour
    {
        public ITarget target { get; private set; }

        public Vector3 VectorToTarget;

        public void SetTarget(ITarget newTarget)
        {
            target = newTarget;
        }
    }
}