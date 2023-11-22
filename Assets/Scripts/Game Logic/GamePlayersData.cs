using SpaceShooter.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.GameLogic
{
    public class GamePlayersData : NetworkBehaviour
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
            if (NetworkManager.Singleton.IsHost)
            {
                foreach (ulong id in NetworkManager.Singleton.ConnectedClientsIds)
                {
                    PlayerState newPlayer = Instantiate(_playerStatePrefab, null);
                    newPlayer.PlayerId = id;
                    newPlayer.GetComponent<NetworkObject>().SpawnWithOwnership(id);
                    _playerStates.Add(newPlayer);
                }
            }
        }
        
        [ServerRpc(RequireOwnership = false)]
        public void SetPlayerDataServerRpc(PlayerData playerData)
        {
            PlayerState playerState = _playerStates.Find(playerState => playerState.PlayerId == playerData.PlayerId);
            
            if (playerState)
                playerState.SetData(playerData);
        }
    }
}
