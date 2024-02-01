using SpaceShooter.GameLogic;
using SpaceShooter.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.User.Configuration
{
    public abstract class ConfigChooser<T> : MonoBehaviour
    {
        public UnityEvent<int> OnPlayerConfigurationUpdated;

        [SerializeField]
        protected Container<T> _container;

        public abstract int GameDataConfigIndex { get; set; }

        private void Start()
        {
            SelectPlayerConfig(GameDataConfigIndex);
        }

        public void SelectNextConfig()
        {
            GameDataConfigIndex++;

            if (GameDataConfigIndex > _container.Items.Length - 1)
            {
                GameDataConfigIndex = 0;
            }

            SelectPlayerConfig(GameDataConfigIndex);
        }

        public void SelectPreviousConfig()
        {
            GameDataConfigIndex--;

            if (GameDataConfigIndex < 0)
            {
                GameDataConfigIndex = _container.Items.Length - 1;
            }

            SelectPlayerConfig(GameDataConfigIndex);
        }

        private void SelectPlayerConfig(int index)
        {
            GameDataConfigIndex = index;
            OnPlayerConfigurationUpdated?.Invoke(index);
        }
    }
}