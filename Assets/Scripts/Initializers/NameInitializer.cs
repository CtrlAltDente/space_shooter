using SpaceShooter.Base;
using SpaceShooter.Interfaces;
using SpaceShooter.User;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class NameInitializer : MonoBehaviour, IInitializer
    {
        [SerializeField]
        private TextMeshProUGUI _name;

        public void Initialize(UserConfig userConfig)
        {
            InitializeName(userConfig.Name);
        }

        private void InitializeName(string name)
        {
            _name.text = name;
        }
    }
}