using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Player
{
    public class Skin : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _skinParts;

        public void SetActoveSkin(bool isActive)
        {
            foreach (GameObject part in _skinParts)
            {
                part.SetActive(isActive);
            }
        }
    }
}