using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter.Spaceships.Destroyer
{
    public class StickInput : MonoBehaviour, IMovementInput
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
                return DirectionChange.action.ReadValue<Vector2>();
            }
        }

        public Vector2 RotationInput
        {
            get
            {
                return RotationChange.action.ReadValue<Vector2>();
            }
        }
    }
}