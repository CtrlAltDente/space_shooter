using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter.Interfaces
{
    public interface IDestroyerInput
    {
        public Vector2 DirectionInput { get; }
        public Vector2 RotationInput { get; }

        public void CalculateInput();
    }
}