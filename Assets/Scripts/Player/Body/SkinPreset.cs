using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class SkinPreset : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _skinParts;

        public void SetActiveSkin(bool isActive)
        {
            foreach (GameObject part in _skinParts)
            {
                part.SetActive(isActive);
            }
        }
    }
}