using SpaceShooter.GameLogic;
using SpaceShooter.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.User
{
    public class UserConfigurator : MonoBehaviour
    {
        [SerializeField]
        private PlayersContainer _playersContainer;

        [SerializeField]
        private TMP_InputField _nameField;

        private void Start()
        {
            _nameField.text = GameData.PlayerName;
        }

        public void SetUserName(string name)
        {
            GameData.PlayerName = name;
        }
    }
}