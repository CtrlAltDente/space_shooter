using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Spaceships.Destroyer
{
    public class DestroyerMovementLogic : MonoBehaviour
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private float _movementSpeed = 30f;
        [SerializeField]
        private float _rotationSpeed = 60f;

        private Vector2 X_Y_Input => _baseInput.DirectionInput;
        private Vector2 Z_Input => _baseInput.RotationInput;

        private IMovementInput _baseInput;

        private void Awake()
        {
            _baseInput = gameObject.GetComponent<IMovementInput>();
        }

        private void Update()
        {
            CalculateMovement();
        }

        private void CalculateMovement()
        {
            _transform.position += (_transform.forward * _movementSpeed * Time.deltaTime);
            _transform.rotation *= (Quaternion.Euler(new Vector3(X_Y_Input.y, X_Y_Input.x, Z_Input.x).normalized * _rotationSpeed * Time.deltaTime));
        }
    }
}