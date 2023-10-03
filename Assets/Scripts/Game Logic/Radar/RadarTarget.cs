using SpaceShooter.Spaceships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.UI.Destroyer
{
    public class RadarTarget : MonoBehaviour
    {
        public DestroyerStatus EnemyDestroyer { get; private set; }

        public Vector3 VectorToTarget;

        public void SetTarget(DestroyerStatus enemyDestroyer)
        {
            EnemyDestroyer = enemyDestroyer;
        }
    }
}