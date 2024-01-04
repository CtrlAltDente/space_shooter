using SpaceShooter.Player;
using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.GameLogic
{
    public class PlayersSpawner : MonoBehaviour
    {
        [SerializeField]
        private PlayerState _playerStatePrefab;

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
                    newPlayer.GetComponent<NetworkObject>().SpawnWithOwnership(id);
                }
            }
        }
    }
}
