using SpaceShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Spaceships.Destroyer
{
    public class DestroyerMovementLogic : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private float _movementSpeed = 30f;
        [SerializeField]
        private float _rotationSpeed = 30f;

        private Vector2 X_Y_Input => _baseInput.DirectionInput;
        private Vector2 Z_Input => _baseInput.RotationInput;

        [SerializeField]
        private IDestroyerMovementInput _baseInput;

        private void Awake()
        {
            _baseInput = gameObject.GetComponent<IDestroyerMovementInput>();
        }

        private void Update()
        {
            CalculateMovement();
        }

        private void CalculateMovement()
        {
            _rigidbody.position += (_rigidbody.transform.forward * _movementSpeed * Time.deltaTime);
            _rigidbody.MoveRotation(Quaternion.Euler(new Vector3(X_Y_Input.y, X_Y_Input.x, Z_Input.x) * _rotationSpeed * Time.deltaTime));
        }
    }
}