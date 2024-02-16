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

        public bool SpawnEnemy(Action robotDestroyCallback)
        {
            if(!HasEnemy)
            {
                _spawnedRobot = Instantiate(_flyingRobotPrefab, transform.position, transform.rotation);
                _spawnedRobot.NetworkObject.Spawn();
                _spawnedRobot.HealthSystem.OnDestroyed.AddListener(() => robotDestroyCallback.Invoke());
                return true;
            }

            return false;
        }
    }
}