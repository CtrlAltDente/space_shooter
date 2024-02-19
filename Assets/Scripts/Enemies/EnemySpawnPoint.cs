using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Enemies
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private FlyingRobot _flyingRobotPrefab;

        [SerializeField]
        private FlyingRobot _spawnedRobot;

        public bool HasEnemy => _spawnedRobot != null;

        public void SpawnEnemy(Action robotDestroyCallback)
        {
            _spawnedRobot = Instantiate(_flyingRobotPrefab, transform.position, transform.rotation);
            _spawnedRobot.NetworkObject.Spawn();
            _spawnedRobot.HealthSystem.OnDestroyed.AddListener(() => robotDestroyCallback.Invoke());
        }
    }
}