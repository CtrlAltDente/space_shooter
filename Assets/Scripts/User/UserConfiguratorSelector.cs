using SpaceShooter.GameLogic;
using SpaceShooter.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.User
{
    public class UserConfiguratorSelector : MonoBehaviour
    {
        public UnityEvent<int> OnPlayerConfigurationUpdated;

        private void Start()
        {
            SelectPlayerConfig(0);
        }

        public void SelectPlayerConfig(int index)
        {
            GameData.PlayerConfigurationIndex = index;
            OnPlayerConfigurationUpdated?.Invoke(index);
        }
    }
}