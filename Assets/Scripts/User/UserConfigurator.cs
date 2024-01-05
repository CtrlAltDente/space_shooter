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
        public UnityEvent<int> OnPlayerConfigurationUpdated;

        [SerializeField]
        private PlayersContainer _playersContainer;

        [SerializeField]
        private TMP_InputField _nameField;

        private void Start()
        {
            _nameField.text = GameData.PlayerName;
            SelectPlayerConfig(GameData.PlayerConfigurationIndex);
        }

        public void SetUserName(string name)
        {
            GameData.PlayerName = name;
        }

        public void SelectNextConfig()
        {
            GameData.PlayerConfigurationIndex++;

            if (GameData.PlayerConfigurationIndex > _playersContainer.Items.Length - 1)
            {
                GameData.PlayerConfigurationIndex = 0;
            }

            SelectPlayerConfig(GameData.PlayerConfigurationIndex);
        }

        public void SelectPreviousConfig()
        {
            GameData.PlayerConfigurationIndex--;

            if (GameData.PlayerConfigurationIndex < 0)
            {
                GameData.PlayerConfigurationIndex = _playersContainer.Items.Length - 1;
            }

            SelectPlayerConfig(GameData.PlayerConfigurationIndex);
        }

        private void SelectPlayerConfig(int index)
        {
            GameData.PlayerConfigurationIndex = index;
            OnPlayerConfigurationUpdated?.Invoke(index);
        }
    }
}