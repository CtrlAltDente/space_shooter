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
        private GamePlayersSpawner _gamePlayersData;
    }
}