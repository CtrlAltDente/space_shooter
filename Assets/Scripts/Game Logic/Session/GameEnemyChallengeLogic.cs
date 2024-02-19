using SpaceShooter.Enemies;
using SpaceShooter.Enums;
using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.GameLogic.Session
{
    public class GameEnemyChallengeLogic : MonoBehaviour, IGameSessionLogic
    {
        private const SessionType SESSION_TYPE = SessionType.EnemyKills;

        [SerializeField]
        private GameSession _gameSession;

        [SerializeField]
        private EnemySpawnPoint[] _enemiesSpawnPoints;

        [SerializeField]
        private SessionConfig _sessionConfig;

        [SerializeField]
        private int _enemiesCount;
        [SerializeField]
        private int _destroyedEnemies;

        public void StartGameSession(SessionConfig sessionConfig)
        {
            if(sessionConfig.Type == SESSION_TYPE)
            {
                _sessionConfig = sessionConfig;
                _enemiesCount = sessionConfig.TypeValue;
                _destroyedEnemies = 0;
                StartCoroutine(HandleSession());
            }
        }

        private IEnumerator HandleSession()
        {
            yield return new WaitForSeconds(2f);

            SpawnEnemies();

            while (_destroyedEnemies < _sessionConfig.TypeValue || AllEnemiesNotDestroyed())
            {
                _gameSession.SetSessionInformation($"Enemies left: {_sessionConfig.TypeValue - _destroyedEnemies}");
                yield return null;
            }

            _gameSession.SetSessionInformation($"Enemies left: {_sessionConfig.TypeValue - _destroyedEnemies}");
            yield return new WaitForSeconds(2f);
            _gameSession.StopGame();
        }

        private void SpawnEnemies()
        {
            foreach (EnemySpawnPoint enemySpawnPoint in _enemiesSpawnPoints)
            {
                if (_enemiesCount > 0)
                {
                    enemySpawnPoint.SpawnEnemy(() => SpawnEnemyOnDestroy(enemySpawnPoint));
                    _enemiesCount--;
                }
            }
        }

        private void SpawnEnemyOnDestroy(EnemySpawnPoint enemySpawnPoint)
        {
            _destroyedEnemies++;

            if(_enemiesCount > 0)
            {
                enemySpawnPoint.SpawnEnemy(() => SpawnEnemyOnDestroy(enemySpawnPoint));
                _enemiesCount--;
            }
        }

        private bool AllEnemiesNotDestroyed()
        {
            foreach(EnemySpawnPoint enemySpawnPoint in _enemiesSpawnPoints)
            {
                if(enemySpawnPoint.HasEnemy)
                    return true;
            }

            return false;
        }
    }
}