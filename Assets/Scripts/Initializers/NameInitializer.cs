using SpaceShooter.Base;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class NameInitializer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _name;

        public void InitializeName(string name, bool isActive)
        {
            _name.text = name;
        }
    }
}