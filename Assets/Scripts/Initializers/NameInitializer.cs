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
        private LookAtCameraText _lookAtCameraText;

        public void InitializeName(string name, bool isActive)
        {
            _lookAtCameraText.Label.text = name;
            _lookAtCameraText.gameObject.SetActive(isActive);
        }
    }
}