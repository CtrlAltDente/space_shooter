using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.Base
{
    public class LookAtCameraText : MonoBehaviour
    {
        public TextMeshProUGUI Label => GetComponentInChildren<TextMeshProUGUI>();

        private void Update()
        {
            LookAtCamera();
        }

        private void LookAtCamera()
        {
            if (Camera.main)
            {
                transform.position = transform.parent.position + Vector3.up;
                transform.LookAt(Camera.main.transform.position);
            }
        }
    }
}