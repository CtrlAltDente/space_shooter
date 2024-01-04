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
        public TMP_InputField _inputField;

        private void Start()
        {
            _inputField.text = GameData.PlayerName;
            SelectPlayerConfig(GameData.PlayerConfigurationIndex);
        }

        public void SelectPlayerConfig(int index)
        {
            GameData.PlayerConfigurationIndex = index;
            OnPlayerConfigurationUpdated?.Invoke(index);
        }

        public void SetUserName(string name)
        {
            GameData.PlayerName = name;
        }
    }
}