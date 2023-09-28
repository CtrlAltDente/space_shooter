using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter.Interfaces
{
    public interface IMovementInput
    {
        public Vector2 DirectionInput { get; }
        public Vector2 RotationInput { get; }
    }
}