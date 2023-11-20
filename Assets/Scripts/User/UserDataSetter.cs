using SpaceShooter.GameLogic;
using SpaceShooter.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.User
{
    public class UserDataSetter : MonoBehaviour
    {
        [SerializeField]
        private LocalUser _localUser;

        [SerializeField]
        private GamePlayersData _gamePlayersData;

        private void Update()
        {
            SetPlayerData();
        }

        private void SetPlayerData()
        {
            try
            {
                SetPlayerBodyReferences();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void SetPlayerBodyReferences()
        {
            _gamePlayersData.SetPlayerDataServerRpc(_localUser.PlayerData);
        }
    }
}