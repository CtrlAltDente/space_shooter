using SpaceShooter.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Spaceships.Destroyer
{
    public class DestroyerMovementLogic : MonoBehaviour
    {
        [SerializeField]
        private float _movementSpeed = 30f;
        [SerializeField]
        private float _rotationSpeed = 30f;

        private Vector2 X_Y_Input => _baseInput.DirectionInput;
        private Vector2 Z_Input => _baseInput.RotationInput;

        [SerializeField]
        private IDestroyerInput _baseInput;

        private void Awake()
        {
            _baseInput = gameObject.GetComponent<IDestroyerInput>();
        }

        private void Update()
        {
            CalculateMovement();
        }

        private void CalculateMovement()
        {
            transform.position += transform.forward * _movementSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(new Vector3(X_Y_Input.y, X_Y_Input.x, Z_Input.x) * _rotationSpeed * Time.deltaTime);
        }
    }
}