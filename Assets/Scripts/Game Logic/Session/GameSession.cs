using SpaceShooter.Data;
using SpaceShooter.Interfaces;
using SpaceShooter.Network;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.GameLogic.Session
{
    public class GameSession : NetworkBehaviour
    {
        public NetworkVariable<FixedString32Bytes> SessionInformation;

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
            SessionInformation.Value = sessionInformation;
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
