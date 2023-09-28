using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.GunSystem;
using UnityEngine.InputSystem;
using SpaceShooter.Interfaces;

namespace SpaceShooter.Spaceships.Destroyer
{
    public class DestroyerFireSystemLogic : MonoBehaviour
    {
        [SerializeField]
        private Gun _destroyerGun;

        private IFireInput _fireInput;

        private void Awake()
        {
            _fireInput = GetComponent<IFireInput>();
        }

        private void Update()
        {
            FireSystemControl();
        }

        private void FireSystemControl()
        {
            if (_fireInput.FireInput)
            {
                _destroyerGun.Shoot();
            }
        }
    }
}