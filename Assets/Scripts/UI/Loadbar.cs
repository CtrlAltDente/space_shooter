using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
    public class Loadbar : MonoBehaviour
    {
        [SerializeField]
        private Image[] _loadbarImages;

        private void Start()
        {
            SetLoadProgress(1f);
        }

        private void OnDisable()
        {
            SetLoadProgress(0f);
        }

        public void SetLoadProgress(float progress)
        {
            foreach (Image loadbarImage in _loadbarImages)
            {
                loadbarImage.fillAmount = progress;
            }
        }
    }
}