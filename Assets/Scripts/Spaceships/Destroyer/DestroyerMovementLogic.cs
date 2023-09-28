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

        private Vector2 LeftHandInput => _baseInput.DirectionInput;
        private Vector2 RightHandInput => _baseInput.RotationInput;

        private IMovementInput _baseInput;

        private void Awake()
        {
            _baseInput = gameObject.GetComponent<IMovementInput>();
        }

        private void Update()
        {
            SpeedControlLogic();
            CalculateMovement();
        }

        private void CalculateMovement()
        {
            _transform.position += (_transform.forward * _movementSpeed * Time.deltaTime);
            _transform.rotation *= (Quaternion.Euler(new Vector3(LeftHandInput.y, LeftHandInput.x, -RightHandInput.x).normalized * _rotationSpeed * Time.deltaTime));
        }

        private void SpeedControlLogic()
        {
            float newSpeed = _movementSpeed + RightHandInput.y * 5f * Time.deltaTime;

            _movementSpeed = Mathf.Clamp(newSpeed, 0f, 30f);
        }
    }
}