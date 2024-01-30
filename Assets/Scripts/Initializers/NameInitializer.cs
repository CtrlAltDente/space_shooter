using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceShooter.Initializers
{
    public class NameInitializer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _nameLabel;

        private void Update()
        {
            LookAtCamera();
        }

        public void InitializeName(string name)
        {
            _nameLabel.text = name;
        }

        private void LookAtCamera()
        {
            if(Camera.main)
            {
                transform.position = transform.parent.position + Vector3.up;
                transform.LookAt(Camera.main.transform.position);
            }
        }
    }
}