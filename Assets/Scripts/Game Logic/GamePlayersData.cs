using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.GameLogic
{
    public class GamePlayersData : MonoBehaviour
    {
        [SerializeField]
        private PlayerState _playerStatePrefab;

        [SerializeField]
        private List<PlayerState> _playerStates = new List<PlayerState>();

        private void Start()
        {
            InitializePlayers();
        }

        private void InitializePlayers()
        {
            _playerStates.Add(Instantiate(_playerStatePrefab, null));
        }

        public void SetPlayerData(PlayerData playerData)
        {
            PlayerState playerState = _playerStates.Find(playerState => playerState.PlayerId == playerData.PlayerId);
            
            if (playerState)
                playerState.SetData(playerData);
        }
    }
}
