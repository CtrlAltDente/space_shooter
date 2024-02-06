using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class LookAtCameraComponent : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _offset;

        private void Update()
        {
            LookAtCamera();
        }

        private void LookAtCamera()
        {
            if (Camera.main)
            {
                transform.position = transform.parent.position + _offset;
                transform.LookAt(Camera.main.transform.position);
            }
        }
    }
}