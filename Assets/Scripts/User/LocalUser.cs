
using SpaceShooter.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SpaceShooter.User
{
    public class LocalUser : MonoBehaviour
    {
        [SerializeField]
        private PlayerState _playerState;

        [SerializeField]
        private PlayerBodyReferences _playerBodyReferences;
        [SerializeField]
        private PlayerInputReferences _playerInputReferences;

        public PlayerData PlayerData
        {
            get
            {
                return new PlayerData(PlayerId, PlayerBodyData, PlayerInputData);
            }
        }

        public ulong PlayerId => NetworkManager.Singleton.IsClient ? NetworkManager.Singleton.LocalClientId : 0;

        public PlayerBodyData PlayerBodyData => _playerBodyReferences.BodyData;
        public PlayerInputData PlayerInputData => _playerInputReferences.InputData;

        private void Start()
        {
            StartCoroutine(FindLocalUserPlayer());
        }

        private void Update()
        {
            SetPlayerData();
        }

        private void SetPlayerData()
        {
            if (_playerState)
            {
                _playerState.SetPlayerData(PlayerData);
            }
        }

        private IEnumerator FindLocalUserPlayer()
        {
            while (!_playerState)
            {
                var players = FindObjectsOfType<PlayerState>();
                foreach (var p in players)
                {
                    if (p.OwnerClientId == PlayerId)
                    {
                        _playerState = p;
                    }

                    yield return null;
                }

                yield return null;
            }
        }
    }
}
