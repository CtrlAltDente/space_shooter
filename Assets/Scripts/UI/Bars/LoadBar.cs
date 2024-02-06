using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
    public class LoadBar : ValueBar
    {
        [SerializeField]
        private Image[] _valueImages;

        private void Start()
        {
            SetBarValue(1f);
        }

        private void OnDisable()
        {
            SetBarValue(0f);
        }

        public override void SetBarValue(float progress)
        {
            foreach (Image loadbarImage in _valueImages)
            {
                loadbarImage.fillAmount = progress;
            }
        }

        public override void SetBarValue(float currentValue, float maxValue)
        {
            foreach(Image loadbarImage in _valueImages)
            {
                loadbarImage.fillAmount = currentValue / maxValue;
            }
        }
    }
}