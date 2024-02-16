using SpaceShooter.Interfaces;
using SpaceShooter.Network;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.GameLogic
{
    public class GameSession : NetworkBehaviour
    {
        private NetworkVariable<string> _sessionInformation;

        [SerializeField]
        private NetworkControl _networkControl;

        private IGameSessionLogic[] _gameSessionLogics;

        private void Awake()
        {
            _gameSessionLogics = GetComponentsInChildren<IGameSessionLogic>();
        }

        private void Start()
        {
            StartGame();
        }

        public void SetSessionInformation(string sessionInformation)
        {
            _sessionInformation.Value = sessionInformation;
        }

        private void StartGame()
        {
            if(NetworkManager.Singleton.IsHost)
            {
                Debug.Log($"game session: {GameData.SessionType}, {GameData.SessionTypeValue}");

                foreach (IGameSessionLogic gameSessionLogic in _gameSessionLogics)
                {
                    gameSessionLogic.StartGameSession(GameData.SessionConfig);
                }
            }
        }

        public void StopGame()
        {
            _networkControl.Shutdown(false);
        }
    }
}
