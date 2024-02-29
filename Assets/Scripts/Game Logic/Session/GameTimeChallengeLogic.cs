using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Enums;
using SpaceShooter.Interfaces;
using SpaceShooter.Enemies;

namespace SpaceShooter.GameLogic.Session
{
    public class GameTimeChallengeLogic : MonoBehaviour, IGameSessionLogic
    {
        private const SessionType SESSION_TYPE = SessionType.Time;
        
        [SerializeField]
        private GameSession _gameSession;

        [SerializeField]
        private EnemySpawnPoint[] _enemiesSpawnPoints;

        [SerializeField]
        private SessionConfig _sessionConfig;

        public void StartGameSession(SessionConfig sessionConfig)
        {
            if (sessionConfig.Type == SESSION_TYPE)
            {
                _sessionConfig = sessionConfig;
                StartCoroutine(HandleSession());
            }
        }

        private IEnumerator HandleSession()
        {
            yield return new WaitForSeconds(2f);
            
            float time = _sessionConfig.TypeValue * 60f;

            while (time > 0)
            {
                CheckEnemies();
                _gameSession.SetSessionInformation($"Seconds left: {time.ToString("#")}");
                yield return new WaitForEndOfFrame();
                time -= Time.deltaTime;
            }

            _gameSession.SetSessionInformation($"Seconds left: 0");
            yield return new WaitForSeconds(2f);
            _gameSession.StopGame();
        }

        private void CheckEnemies()
        {
            foreach (EnemySpawnPoint enemySpawnPoint in _enemiesSpawnPoints)
            {
                if (!enemySpawnPoint.HasEnemy)
                {
                    enemySpawnPoint.SpawnEnemy();
                }
            }
        }
    }
}