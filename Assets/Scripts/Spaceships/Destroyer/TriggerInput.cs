using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter.Spaceships.Destroyer
{
    public class TriggerInput : MonoBehaviour, IFireInput
    {
        [SerializeField]
        private InputActionReference _inputActionReference;

        public bool FireInput
        {
            get
            {
                return _inputActionReference.action.ReadValue<float>() > 0.5f;
            }
        }
    }
}