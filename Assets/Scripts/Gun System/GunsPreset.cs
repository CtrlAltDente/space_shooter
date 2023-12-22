using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Guns
{
    public class GunsPreset : MonoBehaviour
    {
        [SerializeField]
        private Gun[] _guns;

        public void SetActiveGuns(bool isActive)
        {
            foreach (Gun guns in _guns)
            {
                guns.gameObject.SetActive(isActive);
            }
        }
    }
}