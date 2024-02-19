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
        private int _enemiesCount;

        public void StartGameSession(SessionConfig sessionConfig)
        {
            if(sessionConfig.Type == SESSION_TYPE)
            {
                _enemiesCount = sessionConfig.TypeValue;
                StartCoroutine(HandleSession());
            }
        }

        private IEnumerator HandleSession()
        {
            yield return new WaitForSeconds(2f);

            SpawnEnemies();

            while (_enemiesCount > 0 && CheckThatAllEnemiesDestroyed())
            {
                _gameSession.SetSessionInformation($"Enemies left: {_enemiesCount}");
                yield return null;
            }

            _gameSession.SetSessionInformation($"Enemies left: {_enemiesCount}");
            yield return new WaitForSeconds(2f);
            _gameSession.StopGame();
        }

        private void SpawnEnemies()
        {
            int startEnemies = _enemiesCount;

            foreach (EnemySpawnPoint enemySpawnPoint in _enemiesSpawnPoints)
            {
                if (startEnemies > 0)
                {
                    enemySpawnPoint.SpawnEnemy(() => SpawnEnemyOnDestroy(enemySpawnPoint));
                    startEnemies--;
                }
            }
        }

        private void SpawnEnemyOnDestroy(EnemySpawnPoint enemySpawnPoint)
        {
            _enemiesCount--;

            if(_enemiesCount > 0)
            {
                enemySpawnPoint.SpawnEnemy(() => SpawnEnemyOnDestroy(enemySpawnPoint));
            }
        }

        private bool CheckThatAllEnemiesDestroyed()
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