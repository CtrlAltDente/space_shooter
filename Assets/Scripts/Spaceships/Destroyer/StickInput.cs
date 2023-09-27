using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter.Spaceships.Destroyer
{
    public class StickInput : MonoBehaviour, IDestroyerMovementInput
    {
        [SerializeField]
        private InputActionReference DirectionChange;
        [SerializeField]
        private InputActionReference RotationChange;

        [SerializeField]
        private Vector2 _directionInput;

        [SerializeField]
        private Vector2 _rotationInput;

        public Vector2 DirectionInput
        {
            get
            {
                return _directionInput;
            }
        }

        public Vector2 RotationInput
        {
            get
            {
                return -_rotationInput;
            }
        }

        private void Update()
        {
            CalculateInput();
        }

        public void CalculateInput()
        {
            _directionInput = DirectionChange.action.ReadValue<Vector2>();
            _rotationInput = RotationChange.action.ReadValue<Vector2>();
        }
    }
}