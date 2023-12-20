
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
        public ulong PlayerId => NetworkManager.Singleton.IsClient ? NetworkManager.Singleton.LocalClientId : 0;
        public PlayerBodyData PlayerBodyData => new PlayerBodyData(_playerBodyReferences.Head, _playerBodyReferences.LeftHand, _playerBodyReferences.RightHand);
        public PlayerInputData PlayerInputData => new PlayerInputData(_playerInputReferences.LeftTriggerPressed, _playerInputReferences.RightTriggerPressed, _playerInputReferences.LeftStickPressed, _playerInputReferences.RightStickPressed);

        public PlayerData PlayerData
        {
            get
            {
                return new PlayerData(PlayerId, PlayerBodyData, PlayerInputData);
            }
        }

        [SerializeField]
        private PlayerState _playerState;

        [SerializeField]
        private PlayerBodyReferences _playerBodyReferences;
        [SerializeField]
        private PlayerInputReferences _playerInputReferences;

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
                Debug.Log("searching");
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
