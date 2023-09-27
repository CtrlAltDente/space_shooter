using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.GunSystem;
using UnityEngine.InputSystem;

namespace SpaceShooter.Spaceships.Destroyer
{
    public class DestroyerFireSystemLogic : MonoBehaviour
    {
        [SerializeField]
        private Gun _destroyerGun;

        [SerializeField]
        private InputActionReference _fireActionReference;

        private void Update()
        {
            FireSystemControl();
        }

        private void FireSystemControl()
        {
            if (_fireActionReference.action.ReadValue<float>() > 0.5f)
            {
                _destroyerGun.Shoot();
            }
        }
    }
}